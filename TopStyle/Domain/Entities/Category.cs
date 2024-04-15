using System.ComponentModel.DataAnnotations;

namespace TopStyle.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}