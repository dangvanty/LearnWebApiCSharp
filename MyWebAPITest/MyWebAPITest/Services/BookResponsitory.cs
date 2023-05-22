using Microsoft.EntityFrameworkCore;
using MyWebAPITest.Data;
using MyWebAPITest.Helpers;
using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class BookResponsitory : IBookResponsitory
    {
        private readonly MyTestDBContext _context;
        public const int PAGE_SIZE = 3;

        public BookResponsitory(MyTestDBContext context)
        {
            _context = context;
        }

        public List<BookVM > GetAllBook(string search, double? from, double? to, string sortBy, int page)
        {
            var booksSearch = _context.books.Include(b=>b.Category).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                booksSearch = booksSearch.Where(p => p.Name.Contains(search) || p.Title.Contains(search));
            }

            if(from.HasValue)
            {
                booksSearch = booksSearch.Where(p => p.Prices >= from);
            }  
            if(to.HasValue)
            {
                booksSearch = booksSearch.Where((p) => p.Prices <= to);
            }
            #endregion

            #region Sorting
            booksSearch = booksSearch.OrderBy(p => p.Name);

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_asc": booksSearch = booksSearch.OrderBy(p=>p.Name); break;
                    case "name_desc": booksSearch = booksSearch.OrderByDescending(p=>p.Name); break;
                    case "title_asc": booksSearch = booksSearch.OrderBy(p=>p.Title); break;
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
            return result.Select(b => new BookVM
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
            }).ToList();
        }
    }
}
