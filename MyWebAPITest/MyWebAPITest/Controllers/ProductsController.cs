using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPITest.Models;
using MyWebAPITest.Services;

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private  ProductService Products;
        private  ILogger<ProductsController> logger;
        public ProductsController(ProductService _Products,ILogger<ProductsController> _logger ) { 
            Products = _Products;
            logger = _logger;
            logger.LogInformation("Product controller created");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Products);
        }
        [HttpPost]
        public IActionResult Create(ProductModelVM productVm) 
        {
            var product = new ProductModel()
            {
                ProductId = Guid.NewGuid(),
                Name = productVm.Name,
                Description = productVm.Description,
                Price = productVm.Price,
            };
            Products.Add(product);
            return Ok(new
            {
                Success = true,
                data = product
            });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = Products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            // LinQ
           
        }
        [HttpPut("{id}")]
        public IActionResult Edit(string id, ProductModelVM productEdit)
        {
            try
            {
                var product = Products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                if(id!=product.ProductId.ToString())
                {
                    return BadRequest();
                }    
                product.Description = productEdit.Description;
                product.Price = productEdit.Price;
                product.Name = productEdit.Name;
                return Ok(product);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var product = Products.SingleOrDefault(p=>p.ProductId==Guid.Parse(id));
            if(product == null)
            {
                return NotFound();
            }
            Products.Remove(product);
            return Ok(new
            {
                Success= true,
                data= product
            });
        }
    }
}
