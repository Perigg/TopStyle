using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Context
{
    public class TopStyleDbContext : IdentityDbContext<User>
    {
        public TopStyleDbContext(DbContextOptions<TopStyleDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User) 
                .WithMany(u => u.Orders) 
                .HasForeignKey(o => o.UserId) 
                .IsRequired() 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails) 
                .HasForeignKey(od => od.ProductId)  
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}