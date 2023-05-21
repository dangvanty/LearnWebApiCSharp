using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPITest.Data;
using MyWebAPITest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyTestDBContext context;

        public CategoryController(MyTestDBContext _context) {
            context= _context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var category = context.categories.ToList();
            return Ok(category);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var cate = context.categories.SingleOrDefault(c=>c.CategoryID==Guid.Parse(id));
            if(cate==null)
            {
                return NotFound();
            }
            return Ok(cate);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Create([FromBody] CategoryModel ctNew)
        {
            try
            {
                var cteNew = new Category { CategoryID=Guid.NewGuid(), CategoryName=ctNew.CategoryName};
                context.categories.Add(cteNew);
                context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    data = cteNew
                });

            }
            catch (Exception)
            {

                return BadRequest(new
                {
                    Success = false,
                });
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateByID(string id, [FromBody] CategoryModel ctUpdate)
        {
            try
            {
                var ctgory = context.categories.SingleOrDefault(c => c.CategoryID == Guid.Parse(id));
                if (ctgory == null)
                {
                    return NotFound();
                }
                ctgory.CategoryName= ctUpdate.CategoryName;
                context.SaveChanges();
                return Ok(new
                {
                    Success=true
                });
            }
            catch (Exception)
            {

                return BadRequest();
            }
           
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var ctgory = context.categories.SingleOrDefault(c => c.CategoryID == Guid.Parse(id));
                if (ctgory == null)
                {
                    return NotFound();
                }
                context.categories.Remove(ctgory);
                context.SaveChanges();
                return Ok(new { Success = true });
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
