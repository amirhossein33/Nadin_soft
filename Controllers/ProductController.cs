using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myproject.Data;
using myproject.Repository;

namespace myproject.Controllers
{
    [ApiController]
    [Route("[Action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {


        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;

        }


        //متدی برای خواندن تمام محصولات
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ReadProducts()
        {
            var products = await _productRepository.ReadProductsDetails();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ReadProductDetailsById(int id)
        {
            var product = await _productRepository.ReadProductDetailsById(id);
            if (product == null)
            {
                return NotFound("No book was found with this ID");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("please enter the name of book");

            }
            var id = await _productRepository.CreateProduct(product);
            return Ok(id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var updatedProduct = await _productRepository.UpdateProduct(product);

            return Ok(updatedProduct);
        }
        [HttpPatch]
        public async Task<IActionResult> PartialUpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            var updatedProduct = await PartialUpdateProduct(id, patchDocument);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] Product product)
        {
            var deleted = await _productRepository.DeleteProduct(product.ProductId);

            if (!deleted)
            {
                return NotFound("No product was found with this ID");
            }

            return Ok("Product deleted successfully");
        }










    }
}
