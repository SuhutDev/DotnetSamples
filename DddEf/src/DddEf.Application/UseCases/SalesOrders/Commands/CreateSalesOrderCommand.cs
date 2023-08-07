using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands;
public record CreateSalesOrderCommand
(
    string TransNo,
    DateTime TransDate,
    CustomerId customerId,
    List<SalesOrderItemVm> Items

) : IRequest<Guid>;

public record SalesOrderItemVm(
    ProductId ProductId,
    double Qty,
    double Price
);
