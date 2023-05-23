using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWebAPITest.Data;
using MyWebAPITest.Helpers;
using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class BookResponsitory : IBookResponsitory
    {
        private readonly MyTestDBContext _context;
        private readonly IMapper _mapper;
        public const int PAGE_SIZE = 3;

        public BookResponsitory(MyTestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetAllBook(string search, double? from, double? to, string sortBy = "name_asc", int page = 1)
        {
            var booksSearch = _context.books.Include(b => b.Category).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                booksSearch = booksSearch.Where(p => p.Name.Contains(search) || p.Title.Contains(search));
            }

            if (from.HasValue)
            {
                booksSearch = booksSearch.Where((p) => p.Prices >= from);
            }
            if (to.HasValue)
            {
                booksSearch = booksSearch.Where((p) => p.Prices <= to);
            }
            #endregion

            #region Sorting
            booksSearch = booksSearch.OrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_asc": booksSearch = booksSearch.OrderBy(p => p.Name); break;
                    case "name_desc": booksSearch = booksSearch.OrderByDescending(p => p.Name); break;
                    case "title_asc": booksSearch = booksSearch.OrderBy(p => p.Title); break;
                    case "title_desc": booksSearch = booksSearch.OrderByDescending(p => p.Title); break;
                }
            }
            #endregion

            //#region Paging
            //booksSearch = booksSearch.Skip((page-1)*PAGE_SIZE).Take(PAGE_SIZE);
            //#endregion
            //var result = booksSearch.Select(p => new BookVM
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Title = p.Title,
            //    Description = p.Description,
            //    Prices = p.Prices,
            //    Author = p.Author,
            //    CateID = p.CateID,
            //    Discount = p.Discount,
            //    CategoryName = p.Category.CategoryName
            //});
            //return result.ToList();

            var result = PaginatedList<Book>.Create(booksSearch, page, PAGE_SIZE);
            return new
            {
                TotalPage = result.TotalPage,
                PageCurrent = result.PageIndex,
                PageSize = PAGE_SIZE,
                Data = result.Select(b => new BookVM
                {
                    Id = b.Id,
                    Name = b.Name,
                    Title = b.Title,
                    Description = b.Description,
                    Prices = b.Prices,
                    Author = b.Author,
                    CateID = b.CateID,
                    Discount = b.Discount,
                    CategoryName = b.Category?.CategoryName
                }).ToList()
            };
        }

        public async Task<string> CreateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);
            _context.books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id.ToString();
        }

        public async Task UpdateBook(string Id, BookUpdate bookEdit)
        {
            var book = _context.books.SingleOrDefault(b => b.Id == Guid.Parse(Id));

            book.Title = bookEdit?.Title ?? book.Title;
            book.Name = bookEdit?.Name ?? book.Name;
            book.Description = bookEdit?.Description ?? book.Description;
            book.Discount = bookEdit?.Discount ?? book.Discount;
            book.Prices = bookEdit?.Prices ?? book.Prices;
            book.Author = bookEdit?.Author ?? book.Author;
            book.CateID = bookEdit?.CateID ?? book.CateID;

            _context.books.Update(book);
            await _context.SaveChangesAsync();

        }

        public async Task<BookModel> FindBookById(string Id)
        {
            var book = await _context.books.SingleOrDefaultAsync(b => b.Id == Guid.Parse(Id));
            if (book == null)
                return null;

            return _mapper.Map<BookModel>(book);
        }

        public async Task DeleteBookById(string Id)
        {
            var book = await _context.books.SingleOrDefaultAsync(b => b.Id == Guid.Parse(Id));
            if (book != null)
            {
                _context.books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
