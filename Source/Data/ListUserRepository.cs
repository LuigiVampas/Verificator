using System.Collections.Generic;
using System.Linq;
using Model;
using Presentation;

namespace Data
{
    /// <summary>
    /// Репозиторий, в котором хранятся объекты класса User, основанный на List.
    /// </summary>
    public class ListUserRepository : IUserRepository
    {
        /// <summary>
        /// Список пользователей, хранящихся в репозитории.
        /// </summary>
        private readonly List<User> _users;

        /// <summary>
        /// Id последнего добавленного пользователя.
        /// </summary>
        private int _lastId;

        /// <summary>
        /// Создаёт новый репозиторий пользователей.
        /// </summary>
        public ListUserRepository()
        {
            _users = new List<User>();
        }

        /// <summary>
        /// Возвращает объект класса User с заданным id, если он существует.
        /// </summary>
        /// <param name="id">id искомого объекта класса User.</param>
        /// <returns>Объект класса User с заданным id. Если такого нет то null.</returns>
        public User GetUser(int id)
        {
            var singleOrDefault = _users.SingleOrDefault(s => s.Id == id);
            if (singleOrDefault == null)
                return null;

            return (User)singleOrDefault.Clone();
        }

        /// <summary>
        /// Возвращает все объекты класса User, хранящиеся в репозитории.
        /// </summary>
        /// <returns>Все объекты класса User, хранящиеся в репозитории.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        /// <summary>
        /// Обновляет поля указанный объект класса User.
        /// </summary>
        /// <param name="user">Объект класса User, поля которого нужно обновить.</param>
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

        /// <summary>
        /// Удаляет из репозитория указанный объект класса User.
        /// </summary>
        /// <param name="user">Удаляемый объект класса User.</param>
        public void DeleteUser(User user)
        {
            var userFromList = _users.SingleOrDefault(u => u.Id == user.Id);

            if (userFromList == null)
                return;

            _users.Remove(userFromList);
        }

        /// <summary>
        /// Добавляет указанного юзера в репозиторий.
        /// </summary>
        /// <param name="user">Добавляемый объекта класса User.</param>
        public void AddUser(User user)
        {
            if (user.Id == 0)
                user.Id = ++_lastId;

            if (_users.Any(u => u.Id == user.Id))
                return;

            _users.Add(user);
        }
    }
}
