using Microsoft.EntityFrameworkCore;
using RetailApp.API.Paging;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;
using System.Collections.Immutable;

namespace RetailApp.API.Repository
{
    public class ProductApprovalQueueRepository : IProductApprovalQueueRepository
    {

        private readonly RepositoryContext _context;

        public ProductApprovalQueueRepository(RepositoryContext context)
        {
            _context = context;

        }

        public async Task CreateProductApprovalQueue(ProductApprovalQueue productApprovalQueue)
        {
            _context.Add(productApprovalQueue);
            await _context.SaveChangesAsync();
        }

        public Task DeleteProductApprovalQueue(ProductApprovalQueue productApprovalQueue)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductApprovalQueue> GetProductApprovalQueue(int id)
        {
           return await _context.ProductApprovalQueues.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public Task<PagedList<ProductApprovalQueue>> GetProductApprovalQueues(ProductParameters productParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductApprovalQueue>> GetProductApprovalQueues()
        {
            return await _context.ProductApprovalQueues
                            .AsNoTracking()
                            .Where(p => p.IsApproved == false)
                            .Include(ap => ap.Product).Where(p => p.IsApproved == false)
                            .OrderBy(p => p.RequestDate)
                            .ToListAsync();
        }

        public Task UpdateProductApprovalQueue(ProductApprovalQueue productApprovalQueue, ProductApprovalQueue dbProductApprovalQueue)
        {
            throw new NotImplementedException();
        }
    }
}
