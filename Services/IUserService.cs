using Policies.Models;

namespace Policies.Services
{
    public interface IUserService
    {
        User Create(User user);
        List<User> GetAllUsers();
        User Get(string id);
        string GenerateToken(string username);
        bool AuthenticateUser(string username, string password);
    }
}
