using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yarp.ReverseProxy.Transforms;

using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Services.AddAuthorization(options =>
    {
       // options.AddPolicy("myPolicy", policy =>
        //    policy.RequireAuthenticatedUser());
    });
*/

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
            //option 1 : as header; effect on this project and destintion
            /*
            var identity = context.Principal.Identity;
            var userId = identity.Name;
            context.HttpContext.Request.Headers.Add("UserId", userId);
            */
            context.HttpContext.Request.Headers.Add("Header_Name", "Admin");

            //option 2 : as claim; effect only on this project
            /*
            var user = context.Principal;
            // add custom claims to user
            user.Identities.First().AddClaim(new Claim("custom_claim", "custom_value"));
            */
            //context.Principal?.Identities.First().AddClaim(new Claim("CustomClaim_Name", "Admin"));

            //option 3 : as contex item; effect only on this project
            /*
            var claimsIdentity = authentication.Principal.Identity as ClaimsIdentity;
            context.Items["userId"] = claimsIdentity.FindFirst("sub").Value;
            context.Items["userRole"] = claimsIdentity.FindFirst("role").Value;
             */
            //context.HttpContext.Items["HttpContext_Name"] = "Admin";

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
                    transformBuilderContext.AddRequestTransform(async transformContext =>
                    {
                        //no need
                    });
                });


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () =>
{
    return "GatewayApi";
});

app.MapGet("/Secure", () =>
{
    return "GatewayApi-Secure";
}).RequireAuthorization();

app.MapPost("/login", () =>
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
            new Claim(ClaimTypes.Name, "Admin"),
        },
        notBefore: null,
        expires: DateTime.Now.AddYears(1),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}


