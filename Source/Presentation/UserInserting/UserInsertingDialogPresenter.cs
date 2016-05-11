using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    /// <summary>
    /// Презентер диалога добавления пользователя.
    /// </summary>
    public class UserInsertingDialogPresenter : DialogPresenterBase<IUserInsertingDialogView>, IUserInsertingDialogPresenter
    {
        /// <summary>
        /// Возвращает, пользователя с данными, заполненными на диалоговом окне.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Показать диалоговое окно.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        public bool? ShowDialog()
        {
            var result = View.ShowDialog();

            if (result == false) 
                return false;

            User = View.UserDataContext.CreateUser(true);

            return result;
        }
    }
}