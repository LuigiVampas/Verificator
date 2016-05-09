using System.Collections.Generic;
using Model;

namespace Presentation
{
    /// <summary>
    /// Интерфейс репозитория, в котором хранятся объекты класса User.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает объект класса User с заданным id, если он существует.
        /// </summary>
        /// <param name="id">id искомого объекта класса User.</param>
        /// <returns>Объект класса User с заданным id. Если такого нет то null.</returns>
        User GetUser(int id);

        /// <summary>
        /// Возвращает все объекты класса User, хранящиеся в репозитории.
        /// </summary>
        /// <returns>Все объекты класса User, хранящиеся в репозитории.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Добавляет указанного юзера в репозиторий.
        /// </summary>
        /// <param name="user">Добавляемый объекта класса User.</param>
        void AddUser(User user);

        /// <summary>
        /// Удаляет из репозитория указанный объект класса User.
        /// </summary>
        /// <param name="user">Удаляемый объект класса User.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Обновляет поля указанный объект класса User.
        /// </summary>
        /// <param name="user">Объект класса User, поля которого нужно обновить.</param>
        void UpdateUser(User user);
    }
}
