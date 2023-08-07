using DddEf.Domain.Aggregates.Customer;
using DddEf.Infrastructure.Persistence;
using MediatR;

namespace DddEf.Application.UseCases.Customers.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public CreateProductCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create
            (
                 request.CustomerCode,
                 request.CustomerName
            );
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer.Id.Value;
        }
    }
}
