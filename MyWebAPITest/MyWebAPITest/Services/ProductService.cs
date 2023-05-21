using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class ProductService: List<ProductModel>
    {
        public ProductService() {
            this.AddRange(new ProductModel[]
            {
                new ProductModel {ProductId=Guid.NewGuid(), Name="Iphone 10", Description="Day la iPhone 10", Price=1000 },
                new ProductModel {ProductId=Guid.NewGuid(), Name="Samsung", Description="Day la Samsung", Price=700 },
                new ProductModel {ProductId=Guid.NewGuid(), Name="Galaxy", Description="Day la Galaxy", Price=500 },
                new ProductModel {ProductId=Guid.NewGuid(), Name="Nokia", Description="Day la Nokia", Price=800 },
                new ProductModel {ProductId=Guid.NewGuid(), Name="Oppo", Description="Day la Oppo", Price=900 } 
            }
             );
        }
    }
}
