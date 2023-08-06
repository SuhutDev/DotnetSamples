using FluentValidation;

namespace DddEf.Application.UseCases.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.ProductCode).NotEmpty();
            RuleFor(x=>x.ProductName).NotEmpty(); 
        }
    }
}
