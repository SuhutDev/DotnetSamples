using FluentValidation;

namespace DddEf.Application.UseCases.SalesOrders.Commands
{
    public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
    {
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(x=>x.TransNo).NotEmpty();
            RuleFor(x=>x.TransDate).NotEmpty(); 
            RuleFor(x=>x.CustomerId).NotEmpty(); 
            RuleFor(x=>x.Items).NotEmpty(); 
        }
    }
}
