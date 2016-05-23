using System.ComponentModel;

namespace Presentation.Contexts
{
    /// <summary>
    /// Интерфейс контекста представления для изменения пароля.
    /// </summary>
    public interface IPasswordEditContext : INotifyPropertyChanged
    {
        /// <summary>
        /// Получает или возвращает новый пароль. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        string NewPassword { get; set; }

        /// <summary>
        /// Возвращает хэш нового пароля.
        /// </summary>
        /// <returns>Хэш нового пароля.</returns>
        string GetNewPasswordHash();
    }
}