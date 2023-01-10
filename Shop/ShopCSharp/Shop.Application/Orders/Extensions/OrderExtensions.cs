using Shop.Application.Orders.Dtos;
using Shop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Orders.Extensions
{
    public static class OrderExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                MethodPayment = order.MethodPayment,
                Title = order.Title
            };
        }

        public static IEnumerable<OrderDto> ToDtos(
            this IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any())
                return Enumerable.Empty<OrderDto>();

            return orders.Select(x => x.ToDto());
        }
    }
}
