using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    /// <summary>
    /// Интерфейс презентера диалога добавления пользователя.
    /// </summary>
    public interface IUserInsertingDialogPresenter : IDialogPresenter<IUserInsertingDialogView>
    {
        /// <summary>
        /// Возвращает, пользователя с данными, заполненными на диалоговом окне.
        /// </summary>
        User User { get; }

        /// <summary>
        /// Показать диалоговое окно.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        bool? ShowDialog();
    }
}