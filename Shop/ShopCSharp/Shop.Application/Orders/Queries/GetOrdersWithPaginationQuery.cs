using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mappings;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Queries
{
    public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrdersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<OrderDto>> Handle(
            GetOrdersWithPaginationQuery request, 
            CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => x.ToDto())
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return orders;
        }
    }
}
