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
        private readonly IUserRepository _userRepository;

        public UserListPresenter(IUserInsertingDialogPresenter userInsertingDialogPresenter, IUserDeletingDialogPresenter userDeletingDialogPresenter, IUserRepository userRepository)
        {
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
            _userDeletingDialogPresenter = userDeletingDialogPresenter;
            _userRepository = userRepository;
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
                var user = _userInsertingDialogPresenter.User;
                _userRepository.AddUser(user);

                View.Users.Add(user);
            }
        }

        private void OnDeletingUser(object sender, EventArgs e)
        {
            if (_userDeletingDialogPresenter.RunDialog() == true)
                View.Users.Remove(View.SelectedUser);
        }
    }
}
