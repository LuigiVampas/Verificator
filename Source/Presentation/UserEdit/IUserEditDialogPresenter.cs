using Model;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    /// <summary>
    /// Интерфейс презентера, отвечающего за изменение данных пользователя.
    /// </summary>
    public interface IUserEditDialogPresenter : IDialogPresenter<IUserEditDialogView>
    {
        /// <summary>
        /// Создаёт диалог с пользователем об изменении данных пользователя. Возвращает измененного пользователя.
        /// </summary>
        /// <param name="editingUser">Пользователь, данные которого нужно изменить.</param>
        /// <returns>Возвращает изменённого пользователя.</returns>
        User EditUser(User editingUser);
    }
}