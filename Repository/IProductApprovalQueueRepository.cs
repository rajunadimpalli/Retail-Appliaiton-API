using RetailApp.API.Paging;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;

namespace RetailApp.API.Repository
{
    public interface IProductApprovalQueueRepository
    {
        Task<PagedList<ProductApprovalQueue>> GetProductApprovalQueues(ProductParameters productParameters);
        Task<List<ProductApprovalQueue>> GetProductApprovalQueues();
        Task<ProductApprovalQueue> GetProductApprovalQueue(int id);
        Task CreateProductApprovalQueue(ProductApprovalQueue productApprovalQueue);
        Task UpdateProductApprovalQueue(ProductApprovalQueue productApprovalQueue, ProductApprovalQueue dbProductApprovalQueue);
        Task DeleteProductApprovalQueue(ProductApprovalQueue productApprovalQueue);
    }
}
