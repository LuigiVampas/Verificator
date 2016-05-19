using System.ComponentModel;
using Model;

namespace Presentation.Contexts
{
    /// <summary>
    /// Интерфейс контекста представления для ввода данных о пользователе.
    /// </summary>
    public interface IUserDataContext : INotifyPropertyChanged
    {
        /// <summary>
        /// Возвращает id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Получает или возвращает логин. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Получает или возвращает пароль. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Получает или возвращает фамилию. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Surname { get; set; }

        /// <summary>
        /// Получает или возвращает имя. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Получает или возвращает отчество. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Lastname { get; set; }

        /// <summary>
        /// Возвращает инициалы.
        /// </summary>
        string Initials { get;  }

        /// <summary>
        /// Получает или возвращает должность. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string Position { get; set; }

        /// <summary>
        /// Инициализирует контекст новый пользователем.
        /// </summary>
        /// <param name="user">Новый пользователь, связанный с данным контекстом.</param>
        void Initialize(User user);

        /// <summary>
        /// Возвращает пользователя, связанного с данным контекстом.
        /// </summary>
        /// <param name="needHash">Нужно ли возвращать хэш вместо пароля.</param>
        /// <returns></returns>
        User CreateUser(bool needHash);
    }
}