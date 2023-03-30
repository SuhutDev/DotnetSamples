using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UlidAsChar26.SwaggerSetting;

public class GuidParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (parameter?.Schema?.Reference?.Id == "Ulid")
        {
            parameter.Schema.Type = "string";
            parameter.Schema.Format = null;
            parameter.Schema.Reference = null;
        }
    }
}