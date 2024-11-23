using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Restaurant.Models;

namespace Restaurant.Data
{
    public interface IUserRepository
    {
        bool IsExistUserByPhone(string phoneNumber);
        void AddUser(User user);
        User GetUserForLogin(string phoneNumber, string password);
    }

    public class UserRepository : IUserRepository
    {
        RestaurantContext _context;
        public UserRepository(RestaurantContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserForLogin(string phoneNumber, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password);
        }

        public bool IsExistUserByPhone(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }
    }
}
