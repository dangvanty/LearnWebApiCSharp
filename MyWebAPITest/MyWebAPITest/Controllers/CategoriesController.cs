using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAPITest.Models;
using MyWebAPITest.Services;

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private ICategoryResponsitory _categoryRespository;

        public CategoriesController(ICategoryResponsitory categoryRespository) {
        _categoryRespository = categoryRespository;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_categoryRespository.GetAll());
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(string id) 
        {
            try
            {
                var data = _categoryRespository.GetById(id);
                if(data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory(CategoryModel categoryNew)
        {
            try
            {
                var data = _categoryRespository.Create(categoryNew);
                return Ok(data);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory ( string id, CategoryVM category)
        {
            try
            {
                if(id != category.CategoryID.ToString())
                {
                    return NotFound();
                }    
                _categoryRespository.Update(category);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(string id)
        {
            try
            {
                var dataDelete = _categoryRespository.GetById(id);
                if( dataDelete ==null)
                {
                    return NotFound();
                }   
                _categoryRespository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
