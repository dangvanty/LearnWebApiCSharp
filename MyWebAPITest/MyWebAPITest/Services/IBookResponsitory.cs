using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public interface IBookResponsitory
    {
        Task<object> GetAllBook(string search, double? from, double? to, string sortBy, int page);
    }
}
