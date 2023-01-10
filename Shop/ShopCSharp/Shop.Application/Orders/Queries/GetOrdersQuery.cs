using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrdersQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> Handle(
            GetOrdersQuery request, 
            CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .ToListAsync();

            return orders.ToDtos();
        }
    }
}
