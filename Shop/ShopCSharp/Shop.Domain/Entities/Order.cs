
using Shop.Domain.Common;
using Shop.Domain.Enums;

namespace Shop.Domain.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MethodPayment MethodPayment { get; set; }
    }
}
