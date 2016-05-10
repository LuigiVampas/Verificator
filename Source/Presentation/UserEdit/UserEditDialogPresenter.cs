using Model;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    /// <summary>
    /// Презентера диалога изменения пользователя.
    /// </summary>
    public class UserEditDialogPresenter : DialogPresenterBase<IUserEditDialogView>, IUserEditDialogPresenter
    {
        public new bool RunDialog()
        {
            return false;
        }

        public User EditUser(User editingUser)
        {
            View.EditingUser = editingUser;

            if (View.ShowDialog() == true)
                return View.EditingUser;

            return editingUser;
        }
    }
}