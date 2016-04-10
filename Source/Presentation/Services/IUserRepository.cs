using System.Collections.Generic;
using Model;

namespace Presentation.Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}