using Shop.Domain.Enums;

namespace Shop.Application.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MethodPayment MethodPayment { get; set; }
    }
}
