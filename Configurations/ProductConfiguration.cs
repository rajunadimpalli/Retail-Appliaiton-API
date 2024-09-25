using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailApp.Entities.Models;

namespace RetailApp.API.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                //Mugs
                new Product
                {
                    Id = 1,
                    Name = "Travel Mug",
                    Description = @$"Features:
                                    - Features wraparound prints
                                    - Top rack dishwasher safe
                                    - Insulated stainless steel with removable lid
                                    - Mug holds 15oz(443ml)",
                    IsActive = true,
                    Price = 11
                }
            );
        }
    }
}
