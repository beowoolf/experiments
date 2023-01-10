using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteOrderCommand request, 
            CancellationToken cancellationToken)
        {
            _context.Orders.Remove(new Order { Id = request.Id });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
