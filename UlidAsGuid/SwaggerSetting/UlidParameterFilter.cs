using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UlidAsGuid.SwaggerSetting;

public class UlidParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {

        if (parameter?.Schema?.Reference?.Id == "Ulid")
        {
            parameter.Schema.Type = "string";
            parameter.Schema.Format = "uuid";
            parameter.Schema.Reference = null;
        }

    }
}