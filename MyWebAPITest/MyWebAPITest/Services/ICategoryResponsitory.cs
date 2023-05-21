using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public interface ICategoryResponsitory
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(string id);
        CategoryVM Create(CategoryModel category);
        void Update(CategoryVM category);
        void Delete(string id);
    }
}
