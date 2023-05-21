using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPITest.Data;
using MyWebAPITest.Models;

namespace MyWebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private MyTestDBContext _context;

        public BooksController(MyTestDBContext context) {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll() {
            var bookList = _context.books.ToList();
            return Ok(bookList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            var book = _context.books.SingleOrDefault(b => b.Id == Guid.Parse(id));
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Create(BookModel newBook)
        {
            try
            {
                var book = new Book { 
                    Id = Guid.NewGuid(),
                    Author=newBook.Author,
                    Name=newBook.Name,
                    Description=newBook.Description,
                    Discount=newBook.Discount,
                    Prices=newBook.Prices,
                    Title=newBook.Title,
                    CateID=newBook.CateID                
                };
                _context.books.Add(book);
                _context.SaveChanges();
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
        public IActionResult UpdateBook(string id,BookModel bookEdit)
        {
            try
            {
                var book = _context.books.SingleOrDefault(b => b.Id == Guid.Parse(id));
                if (book == null) { return NotFound(); }
                book.Title = bookEdit.Title;
                book.Description = bookEdit.Description;
                book.Discount = bookEdit.Discount;
                book.Prices = bookEdit.Prices;
                book.Author = bookEdit.Author;
                book.CateID = bookEdit.CateID;
                
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
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
            }
        }
        [HttpDelete("id")]
        public IActionResult DeleteBook(string id)
        {
            try
            {
                var book = _context.books.SingleOrDefault(b => b.Id == Guid.Parse(id));
                if (book == null) { return NotFound(); };

                _context.books.Remove(book);
                _context.SaveChanges();
                return Ok(new { Success = true });
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
