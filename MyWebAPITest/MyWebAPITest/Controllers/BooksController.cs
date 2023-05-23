using Microsoft.AspNetCore.Mvc;
using MyWebAPITest.Helpers;
using MyWebAPITest.Models;
using MyWebAPITest.Services;

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookResponsitory _bookResponsitory;

        public BooksController(IBookResponsitory bookResponsitory)
        {
            _bookResponsitory = bookResponsitory;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllBySearch(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            try
            {
                var result = await _bookResponsitory.GetAllBook(search, from, to, sortBy, page);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //public IActionResult GetAll() {
        //    var bookList = _context.books.ToList();
        //    return Ok(bookList);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var book = await _bookResponsitory.FindBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookModel newBook)
        {
            try
            {
                var book = await _bookResponsitory.CreateBook(newBook);

                return Ok(new
                {
                    Succes = true,
                    data = book
                });

            }
            catch (Exception e)
            {

                return BadRequest(new
                {
                    Success = false,
                    Error = e.Message
                });
            };
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookByID(string id, BookUpdate bookEdit)
        {
            try
            {
                if(id != bookEdit.Id.ToString())
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = $"Id sua khong trung khop",
                    });
                }
                _bookResponsitory.UpdateBook(id, bookEdit);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = $"Sua thanh cong tai id {id}"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Success = false,
                    Error = e.Message
                });
            }
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                await _bookResponsitory.DeleteBookById(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = $"Xoa thanh cong tai id {id}"
                });
            }
            catch (Exception e)
            {

                return BadRequest(new
                {
                    Success = false,
                    Error = e.Message
                });
            }

        }

    }
}
