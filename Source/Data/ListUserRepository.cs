using System.Collections.Generic;
using System.Linq;
using Model;
using Presentation;

namespace Data
{
    public class ListUserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public ListUserRepository()
        {
            _users = new List<User>();
        }

        public User GetUser(int id)
        {
            var singleOrDefault = _users.SingleOrDefault(s => s.Id == id);
            if (singleOrDefault == null) 
                return null;

            return (User)singleOrDefault.Clone();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        public void UpdateUser(User user)
        {
            var userFromList = _users.SingleOrDefault(u => u.Id == user.Id);

            if (userFromList == null)
                return;

            userFromList.Name = user.Name;
            userFromList.Surname = user.Surname;
            userFromList.Lastname = user.Lastname;
            userFromList.Login = user.Login;
            userFromList.Password = user.Password;
            userFromList.Position = user.Position;
        }

        public void DeleteUser(User user)
        {
            var userFromList = _users.SingleOrDefault(u => u.Id == user.Id);

            if (userFromList == null)
                return;

            _users.Remove(userFromList);
        }

        public void AddUser(User user)
        {
            if (_users.Any(u => u.Id == user.Id))
                return;

            _users.Add(user);
        }
    }
}
