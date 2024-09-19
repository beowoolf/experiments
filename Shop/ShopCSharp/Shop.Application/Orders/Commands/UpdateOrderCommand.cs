using MediatR;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;
using Shop.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MethodPayment MethodPayment { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateOrderCommand request, 
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            order.Title = request.Title;
            order.MethodPayment = request.MethodPayment;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
