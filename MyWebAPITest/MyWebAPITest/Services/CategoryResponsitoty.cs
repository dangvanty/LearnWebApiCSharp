using MyWebAPITest.Data;
using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class CategoryResponsitoty : ICategoryResponsitory
    {
        private readonly MyTestDBContext _context;

        public CategoryResponsitoty(MyTestDBContext context) {
            _context = context;
        }
        public CategoryVM Create(CategoryModel category)
        {
            var cteNew = new Category
            {
                CategoryID = Guid.NewGuid(),
                CategoryName=category.CategoryName
            };
            _context.Add(cteNew);
            _context.SaveChanges();
            return new CategoryVM
            {
                CategoryID = cteNew.CategoryID,
                CategoryName = cteNew.CategoryName
            };
        }

        public void Delete(string id)
        {
            var cateDelete = _context.categories.SingleOrDefault(p=>p.CategoryID==Guid.Parse(id));
            if(cateDelete != null)
            {
                _context.categories.Remove(cateDelete);
                _context.SaveChanges();
            }  
        }

        public List<CategoryVM> GetAll()
        {
            var  listCate = _context.categories.Select(c=>new CategoryVM { CategoryID = c.CategoryID, CategoryName = c.CategoryName });
            return listCate.ToList();
        }

        public CategoryVM GetById(string id)
        {
            var category = _context.categories.SingleOrDefault(p => p.CategoryID == Guid.Parse(id));
            if(category == null) { return null; };

            return new CategoryVM { 
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName 
            };

        }

        public void Update(CategoryVM categoryEdit)
        {
            var cate = _context.categories.SingleOrDefault(p => p.CategoryID == categoryEdit.CategoryID);
            
            cate.CategoryName = categoryEdit.CategoryName;
            _context.SaveChanges();

        }
    }
}
