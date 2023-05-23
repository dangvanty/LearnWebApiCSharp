using MyWebAPITest.Data;
using MyWebAPITest.Models;

namespace MyWebAPITest.Services
{
    public class UserResponsitory : IUserResponsitory
    {
        private readonly MyTestDBContext _context;

        public UserResponsitory(MyTestDBContext context) {
            _context = context;
        }
        public User Login(LoginModel userLogin)
        {
            var user = _context.users.SingleOrDefault(u => u.Email == userLogin.UserNameOrEmail

            || u.UserName == userLogin.UserNameOrEmail
            && u.Password == userLogin.Password
            );
            return user;
        }
    }
}
