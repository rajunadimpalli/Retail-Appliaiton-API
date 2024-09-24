using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RetailApp.Entities.Models;

namespace RetailApp.API.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) 
        { 
                
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductApprovalQueue> ProductApprovalQueues { get; set; }
    }
}
