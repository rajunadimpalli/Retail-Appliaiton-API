using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailApp.API.Repository;
using RetailApp.Entities.Models;
using RetailApp.Entities.RequestFeatures;
using System.Text.Json;

namespace RetailApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductApprovalQueueRepository _productApprovalQueueRepository;
        public ProductsController(IProductRepository productRepository, IProductApprovalQueueRepository productApprovalQueueRepository)
        {
            _productRepository = productRepository;
            _productApprovalQueueRepository = productApprovalQueueRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
        {
            var products = await _productRepository.GetProducts(productParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(products.MetaData));

            return Ok(products);
        }

        
        [HttpGet("collection/approvalqueue", Name = "GetProductApprovalQueue")]
        public async Task<IActionResult> GetProductApprovalQueue()
        {
            var approvalQueueProducts = await _productApprovalQueueRepository.GetProductApprovalQueues();
            return Ok(approvalQueueProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            //model validation
            if (product is null)
                return BadRequest("Product has not been set");
            if(product.Price >= 10000)
                return BadRequest("Product creation is not allowed when its price is more than 10000 dollars");

            await _productRepository.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var dbProduct = await _productRepository.GetProduct(id);
            if (dbProduct is null)
                return NotFound();

            await _productRepository.UpdateProduct(product, dbProduct);
            return Ok(await _productRepository.GetProducts(new ProductParameters()));
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product is null)
                return NotFound();

            await _productRepository.DeleteProduct(product);
            return Ok(await _productRepository.GetProducts(new ProductParameters()));
        }

    }
}
