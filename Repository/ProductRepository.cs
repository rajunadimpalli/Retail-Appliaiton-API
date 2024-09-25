using Microsoft.EntityFrameworkCore;
using RetailApp.API.Paging;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;

namespace RetailApp.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext _context;
        private readonly IProductApprovalQueueRepository _productApprovalQueueRepository;

        public ProductRepository(RepositoryContext context, IProductApprovalQueueRepository productApprovalQueueRepository)
        {
            _context = context;
            _productApprovalQueueRepository = productApprovalQueueRepository;   
        }
        public async Task CreateProduct(Product product)
        {
            if (product.Price >= 5000)
            {
                product.IsActive = false;
                _context.Add(product);
                await _context.SaveChangesAsync();
                var productApprovalQueue = new ProductApprovalQueue()
                {
                    ProductId = product.Id,
                    Reason = "Any product should be pushed to approval queue if its price is more than 5000 dollars",
                    RequestDate = DateTime.Now
                };
                await _productApprovalQueueRepository.CreateProductApprovalQueue(productApprovalQueue);
            }
            else
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts() =>
            await _context.Products.AsNoTracking().Where(p => p.IsActive == true).ToListAsync();

        public async Task<Product> GetProduct(int id) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<PagedList<Product>> GetProducts(ProductParameters productParameters)
        {
            var products = await _context.Products.AsNoTracking().Where(p => p.IsActive == true)
               .Search(productParameters.SearchTerm)
               .FilterBy(productParameters)
               .Sort(productParameters.OrderBy)
               .ToListAsync();

            return PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }
        /// <summary>
        /// Update product state and run the business rules.
        /// </summary>
        /// <param name="product">product object from client</param>
        /// <param name="dbProduct">existing product entity by id</param>
        /// <returns> void </returns>
        public async Task UpdateProduct(Product product, Product dbProduct)
        {
            var change = ((product.Price - dbProduct.Price) / dbProduct.Price) * 100;
            //Any product should be pushed to approval queue if its price is more than 50% of its previous price.
            if (change > 50)
            {

                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.Description = product.Description;
                dbProduct.IsActive = false;
                var productApprovalQueue = new ProductApprovalQueue()
                {
                    ProductId = dbProduct.Id,
                    Reason = "Any product should be pushed to approval queue if its price is more than 50% of its previous price.",
                    RequestDate = DateTime.Now
                };
                await _productApprovalQueueRepository.CreateProductApprovalQueue(productApprovalQueue);
            }
            else
            {
                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.Description = product.Description;
                if(product.IsActive == true)
                {
                    // get 
                    var productApprovalQueue = await _productApprovalQueueRepository.GetProductApprovalQueue(product.Id);
                    productApprovalQueue.IsApproved = true;
                }
               
            }
            await _context.SaveChangesAsync();
        }
    }
}
