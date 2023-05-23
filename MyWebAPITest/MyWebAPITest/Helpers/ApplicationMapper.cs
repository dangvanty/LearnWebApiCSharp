using AutoMapper;
using MyWebAPITest.Data;
using MyWebAPITest.Models;

namespace MyWebAPITest.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
        
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
