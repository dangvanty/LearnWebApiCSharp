using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public interface IBookResponsitory
    {
        List<BookVM> GetAllBook(string search, double? from, double? to, string sortBy, int page);
    }
}
