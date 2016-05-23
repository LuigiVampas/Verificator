using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    /// <summary>
    /// Интерфейс презентера, отвечающего за изменение пароля пользователя.
    /// </summary>
    public interface IPasswordEditPresenter : IDialogPresenter<IPasswordEditView>
    {
        /// <summary>
        /// Создаёт диалог с пользователем о замене пароля. Возвращает хэш нового пароля.
        /// </summary>
        /// <param name="oldPassword">Старый пароль.</param>
        /// <returns>Хэш нового пароля.</returns>
        string EditPassword(string oldPassword);
    }
}
