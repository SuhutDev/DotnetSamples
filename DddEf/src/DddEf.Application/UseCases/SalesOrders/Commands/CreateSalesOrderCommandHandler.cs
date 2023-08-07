using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Infrastructure.Persistence;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands
{
    public sealed class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public CreateSalesOrderCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var rowNumber = 1;
            var salesOrder = SalesOrder.Create(
             request.TransNo,
             request.TransDate,
             request.CustomerId,
             request.ShipAddress,
             request.BillAddress,
             request.Items.ConvertAll(item => SalesOrderItem.Create(
                 rowNumber++,
                 item.ProductId,
                 item.Qty,
                 item.Price
                 )));

            await _dbContext.SalesOrders.AddAsync(salesOrder);
            await _dbContext.SaveChangesAsync();

            return salesOrder.Id.Value;
        }
    }
}
