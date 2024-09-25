using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailApp.Entities.Models;

namespace RetailApp.API.Configurations
{
    public class ProductApprovalQueueConfiguration : IEntityTypeConfiguration<ProductApprovalQueue>
    {
        public void Configure(EntityTypeBuilder<ProductApprovalQueue> builder)
        {
            builder.HasData(new ProductApprovalQueue
            {
                Id = 2,
                ProductId = 1,
                Reason="price is > 10000",
                IsApproved=false,
                RequestDate = DateTime.Now,

            });
        }
    }
}
