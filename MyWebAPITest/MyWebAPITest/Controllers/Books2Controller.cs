using Microsoft.AspNetCore.Mvc;
using MyWebAPITest.Services;

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Books2Controller : Controller
    {
        private readonly IBookResponsitory _bookResponsitory;

        public Books2Controller(IBookResponsitory bookResponsitory)
        {
            _bookResponsitory = bookResponsitory;
        }
        [HttpGet]
        public IActionResult GetAllBySearch(string?search, double? from, double? to, string? sortBy , int page=1)
        {
            try
            {
                var result = _bookResponsitory.GetAllBook(search, from, to, sortBy, page);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
    }
}
