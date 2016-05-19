using System;
using Model;
using Presentation.Contexts;
using Presentation.MVP;
using Presentation.PasswordEdit;

namespace Presentation.UserEdit
{
    /// <summary>
    /// Презентера диалога изменения пользователя.
    /// </summary>
    public class UserEditDialogPresenter : DialogPresenterBase<IUserEditDialogView>, IUserEditDialogPresenter
    {
        private readonly IPasswordEditPresenter _passwordEditPresenter;
        private readonly Func<IUserDataContext> _userDataContextFactory;

        public UserEditDialogPresenter(IPasswordEditPresenter passwordEditPresenter, Func<IUserDataContext> userDataContextFactory)
        {
            _passwordEditPresenter = passwordEditPresenter;
            _userDataContextFactory = userDataContextFactory;
        }

        protected override void OnViewLoaded()
        {
            View.EditPassword += OnEditPassword;
        }

        public new bool RunDialog()
        {
            return false;
        }

        public User EditUser(User editingUser)
        {
            var dataContext = _userDataContextFactory();
            dataContext.Initialize(editingUser);

            View.UserDataContext = dataContext;

            if (View.ShowDialog() == true)
            {
                var resultUser = View.UserDataContext.CreateUser(false);
                editingUser.Name = resultUser.Name;
                editingUser.Surname = resultUser.Surname;
                editingUser.Password = resultUser.Password;
                editingUser.Lastname = resultUser.Lastname;
                editingUser.Position = resultUser.Position;
            }

            return editingUser;
        }

        private void OnEditPassword(object sender, EventArgs e)
        {
            var user = View.UserDataContext.CreateUser(false);
            user.Password = _passwordEditPresenter.EditPassword(user.Password);
            View.UserDataContext.Initialize(user);
        }
    }
}