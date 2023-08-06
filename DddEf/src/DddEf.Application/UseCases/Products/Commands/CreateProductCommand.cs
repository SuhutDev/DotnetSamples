using MediatR;

namespace DddEf.Application.UseCases.Products.Commands;
public record CreateProductCommand
(
   string ProductCode,
   string ProductName
) : IRequest<Guid>;
