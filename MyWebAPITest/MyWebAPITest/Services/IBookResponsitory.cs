using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public interface IBookResponsitory
    {
        Task<object> GetAllBook(string search, double? from, double? to, string sortBy, int page);
        Task<string> CreateBook( BookModel book);
        Task UpdateBook(string Id, BookUpdate book);
        Task<BookModel> FindBookById(string Id);
        Task DeleteBookById(string Id);
        
    }
}
