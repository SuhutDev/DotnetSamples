using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UlidAsGuid.SwaggerSetting;

public class UlidDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var schema in swaggerDoc.Components.Schemas.Values)
        {
            foreach (var property in schema.Properties.Values)
            {
                if (property?.Reference?.Id == "Ulid")
                {
                    property.Type = "string";
                    property.Format = "uuid";
                    property.Reference = null;
                }

            }
        }
    }
}






