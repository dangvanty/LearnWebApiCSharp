using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class CategoryRespositoryMemory : ICategoryResponsitory
    {
        static List<CategoryVM> _listCate = new List<CategoryVM> {
            new CategoryVM{CategoryID = Guid.NewGuid(), CategoryName = "iPhoneX" },
            new CategoryVM{CategoryID = Guid.NewGuid(), CategoryName = "Samsung" },
            new CategoryVM{CategoryID = Guid.NewGuid(), CategoryName = "Galaxy" },
            new CategoryVM{CategoryID = Guid.NewGuid(), CategoryName = "Nokia" }

        };
        public CategoryVM Create(CategoryModel category)
        {
            var newCate = new CategoryVM { CategoryID = Guid.NewGuid(), CategoryName = category.CategoryName };
            _listCate.Add(newCate);
            return newCate;
        }

        public void Delete(string id)
        {
            var cateDelete = _listCate.SingleOrDefault(p=>p.CategoryID == Guid.Parse(id));
            if (cateDelete != null)
            {
                _listCate.Remove(cateDelete);
            }
        }

        public List<CategoryVM> GetAll()
        {
            return _listCate;
        }

        public CategoryVM GetById(string id)
        {
            return _listCate.SingleOrDefault(p=>p.CategoryID== Guid.Parse(id)); 
            
        }

        public void Update(CategoryVM category)
        {
            var cate = _listCate.SingleOrDefault(p => p.CategoryID == category.CategoryID);
            if (cate != null) {
                cate.CategoryName = category.CategoryName;
            }
        }
    }
}
