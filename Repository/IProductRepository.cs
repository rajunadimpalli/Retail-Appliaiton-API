using RetailApp.API.Paging;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;

namespace RetailApp.API.Repository
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product, Product dbProduct);
        Task DeleteProduct(Product product);
    }
}
