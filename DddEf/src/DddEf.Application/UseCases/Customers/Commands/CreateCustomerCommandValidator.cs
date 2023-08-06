using FluentValidation;

namespace DddEf.Application.UseCases.Customers.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.CustomerCode).NotEmpty();
            RuleFor(x=>x.CustomerName).NotEmpty(); 
        }
    }
}
