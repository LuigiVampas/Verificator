using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using Presentation;

namespace Data
{
    class DbUserRepository : IUserRepository

    {
        readonly UserDbContext _context = new UserDbContext();
        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (_context.Users.Contains(user))
                _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            if (_context.Users.Any(u => u.Id == user.Id))
                _context.Entry(user).State = EntityState.Modified;
        }

    }
}
