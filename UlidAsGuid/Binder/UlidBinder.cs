using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UlidAsGuid.Binder;


public class UlidBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (bindingContext.ModelType.Name == "Ulid")
            {
                var guid = Guid.Parse(value);
                var ulid = new Ulid(guid);
                bindingContext.Result = ModelBindingResult.Success(ulid);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"{bindingContext} property {value} format error.");
            }
            return Task.CompletedTask;
        }
        return Task.CompletedTask;
    }
}

