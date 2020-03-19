using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>().HasKey(i => new { i.OrderId, i.ProductId });

            modelBuilder.Entity<OrderProduct>().HasOne(o => o.Order).WithMany(p => p.OrderProduct).
                HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderProduct>().HasOne(p => p.Product).WithMany(o => o.OrderProduct).
                HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
