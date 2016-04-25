using System.Collections.Generic;
using Model;

namespace Presentation
{
    public interface IUserRepository
    {
        User GetUser(int id);

        IEnumerable<User> GetAllUsers();

        void AddUser(User user);

        void DeleteUser(User user);

        void UpdateUser(User user);
    }
}
