using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TopStyle.Domain.Entities
{
    public class User : IdentityUser
    {
        
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}