using DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Interface;

namespace DummyApi.Controllers
{
    public class ProductController : ODataController
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        
        [HttpGet("odata/Products")]
        [EnableQuery]
        public IActionResult GetAllProducts()
        {
            return Ok(_service.GetAllQueryable());
        }

        [HttpGet("Products({id})")]
        [EnableQuery]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
        {
            return Ok(await _service.CreateAsync(request));
        }

        [HttpPut("Product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest request)
        {
            return Ok(await _service.UpdateAsync(request));
        }

        [HttpDelete("Product({id})")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [HttpDelete("Product/Soft-delete({id})")]
        public async Task<IActionResult> SoftDeleteAsync(int id)
        {
            return Ok(await _service.SoftDeleteAsync(id));
        }
    }
}
