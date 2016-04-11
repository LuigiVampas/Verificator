using System;
using Presentation.MVP;
using Presentation.UserInserting;

namespace Presentation.UserList
{
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        private readonly IUserInsertingDialogPresenter _userInsertingDialogPresenter;

        public UserListPresenter(IUserInsertingDialogPresenter userInsertingDialogPresenter)
        {
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
        }

        protected override void OnViewLoaded()
        {
            View.InsertingUser += OnInsertingUser;
        }

        private void OnInsertingUser(object sender, EventArgs e)
        {
            var okButtonPressed = _userInsertingDialogPresenter.ShowDialog() == true;

            if (okButtonPressed)
            {
                var user = _userInsertingDialogPresenter.View.User;
            }
        }
    }
}
