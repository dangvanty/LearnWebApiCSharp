using MyWebAPITest.Data;
using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public interface IUserResponsitory
    {
        User Login(LoginModel userLogin); 
    }
}
