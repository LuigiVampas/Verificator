using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    /// <summary>
    /// Интерфейс вида, отвечающего за изменение пароля пользователя.
    /// </summary>
    public interface IPasswordEditView : IDialogView
    {
        /// <summary>
        /// Получает или задаёт контекст представления для изменения пароля.
        /// </summary>
        IPasswordEditContext PasswordDataContext { get; set; }
    }
}