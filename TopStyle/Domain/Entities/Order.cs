using System.ComponentModel.DataAnnotations;

namespace TopStyle.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}