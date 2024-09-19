using MediatR;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;
using Shop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IApplicationDbContext _context;

        public GetOrderByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Handle(
            GetOrderByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            return order.ToDto();
        }
    }
}
