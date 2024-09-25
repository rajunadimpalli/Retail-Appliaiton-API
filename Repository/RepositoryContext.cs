using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RetailApp.API.Configurations;
using RetailApp.Entities.Models;
using System.Reflection.Metadata;

namespace RetailApp.API.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) 
        { 
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //.HasOne(e => e.ProductApprovalQueue)
            //.WithOne(e => e.Product)
            //.HasForeignKey<ProductApprovalQueue>(e => e.ProductId)
            //.IsRequired(false);

            //modelBuilder.Entity<ProductApprovalQueue>()
            //.HasOne(e => e.Product)
            //.WithOne(e => e.ProductApprovalQueue)
            //.HasForeignKey<ProductApprovalQueue>(e => e.ProductId)
            //.IsRequired(false);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductApprovalQueueConfiguration());
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductApprovalQueue> ProductApprovalQueues { get; set; }
    }
}
