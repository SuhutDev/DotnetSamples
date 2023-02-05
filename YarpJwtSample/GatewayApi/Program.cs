using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("myPolicy", policy =>
                    {
                        policy
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAssertion(context => context.User.HasClaim(c =>
                                    c.Subject?.Name == "Admin"))
                            .RequireAuthenticatedUser();
                    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie()
.AddJwtBearer(options =>
{

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {

                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            //can't read claim here
            //only for hardcode value 

            //option 1 : as header; effect on this project and destintion 
            // context!.HttpContext.Request.Headers.Add("Header_UserId", userId);

            //option 2 : as claim; effect only on this project 
            //context.Principal?.Identities.First().AddClaim(new Claim("CustomClaim_UserId", 1));

            //option 3 : as contex item; effect only on this project
            //context.HttpContext.Items["HttpContext_UserId"] = 1; 

            return Task.CompletedTask;
        }
    };

    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "practical aspnetcore",
        ValidAudience = "https://localhost:5068/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is custom key for practical aspnetcore sample"))
    };
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
                .AddTransforms(transformBuilderContext =>  // Add transforms inline
                {
                    transformBuilderContext.AddRequestTransform(transformContext =>
                    {
                        //no need
                        var identity = transformContext?.HttpContext?.User.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            IList<Claim> claims = identity.Claims.ToList();
                            if (claims.Count > 0)
                            {
                                var userId = claims[0].Value;
                                var name = claims[0].Value;
                                transformContext!.ProxyRequest.Headers.Add("Header_UserId", userId);
                                System.Console.WriteLine($"test:{userId}");
                            }
                        }

                        return ValueTask.CompletedTask;
                    });

                });


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () =>
{
    return "GatewayApi";
});

app.MapGet("/TestSecure", () =>
{
    return "GatewayApi-TestSecure";
}).RequireAuthorization();

app.MapPost("/Auth/login", () =>
{
    var accessToken = GenerateJSONWebToken();
    var refreshToken = "refreshToken01";

    var result = new
    {
        UserAccess = new
        {
            //xxx
        },
        Token = new
        {
            accessToken = accessToken,
            refreshToken = refreshToken
        },

    };

    return Results.Ok(result);
});

app.MapGet("/Token/Revoke", () =>
{
    return "GatewayApi-Token/Revoke";
}).RequireAuthorization();

app.MapGet("/Token/Refresh", () =>
{
    return "GatewayApi-Token/Refresh";
});


app.MapReverseProxy();

app.Run();

//https://github.com/dodyg/practical-aspnetcore/blob/net6.0/projects/.net7/authentication-1/Program.cs
string GenerateJSONWebToken()
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is custom key for practical aspnetcore sample"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(issuer: "practical aspnetcore",
        audience: "https://localhost:5068/",
        claims: new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Admin"),
        },
        notBefore: null,
        expires: DateTime.Now.AddYears(1),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}


