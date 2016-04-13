using System;
using Presentation.MVP;
using Presentation.UserDeleting;
using Presentation.UserInserting;

namespace Presentation.UserList
{
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        private readonly IUserInsertingDialogPresenter _userInsertingDialogPresenter;
        private readonly IUserDeletingDialogPresenter _userDeletingDialogPresenter;

        public UserListPresenter(IUserInsertingDialogPresenter userInsertingDialogPresenter, IUserDeletingDialogPresenter userDeletingDialogPresenter)
        {
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
            _userDeletingDialogPresenter = userDeletingDialogPresenter;
        }

        protected override void OnViewLoaded()
        {
            View.InsertingUser += OnInsertingUser;
            View.DeletingUser += OnDeletingUser;
        }

        private void OnInsertingUser(object sender, EventArgs e)
        {
            var okButtonPressed = _userInsertingDialogPresenter.ShowDialog() == true;

            if (okButtonPressed)
            {
                var user = _userInsertingDialogPresenter.View.User;
            }
        }

        private void OnDeletingUser(object sender, EventArgs e)
        {
            if (_userDeletingDialogPresenter.RunDialog() == true)
                View.Users.Remove(View.SelectedUser);
        }
    }
}
