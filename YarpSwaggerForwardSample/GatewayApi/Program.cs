using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .AddTransforms(builderContext =>
     {
         builderContext.AddXForwarded(ForwardedTransformActions.Set);
         builderContext.AddXForwardedFor(headerName: "X-Forwarded-For", ForwardedTransformActions.Append);
         builderContext.AddXForwardedHost(headerName: "X-Forwarded-Host", ForwardedTransformActions.Append);
         builderContext.AddXForwardedProto(headerName: "X-Forwarded-Proto", ForwardedTransformActions.Append);
         builderContext.AddXForwardedPrefix(headerName: "X-Forwarded-Prefix", ForwardedTransformActions.Remove);

     })
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    ;

var app = builder.Build();
app.MapGet("/", () =>
{
    return "GatewayApi";
});

app.MapReverseProxy();

app.Run();

