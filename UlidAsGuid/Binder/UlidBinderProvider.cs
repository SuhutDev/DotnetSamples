using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace UlidAsGuid.Binder;


public class UlidBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType.Name == "Ulid")
        {
            return new BinderTypeModelBinder(typeof(UlidBinder));
        }

        return null;
    }
}

