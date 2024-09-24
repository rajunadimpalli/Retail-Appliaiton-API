using RetailApp.API.Paging;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;

namespace RetailApp.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Product>> GetProducts(ProductParameters productParameters)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product, Product dbProduct)
        {
            throw new NotImplementedException();
        }
    }
}
