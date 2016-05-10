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
            var dataContext = new UserDataContext();
            dataContext.Initialize(editingUser);

            View.UserDataContext = dataContext;

            if (View.ShowDialog() == true)
                return View.UserDataContext.CreateUser();

            return editingUser;
        }
    }
}