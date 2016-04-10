using System;
using Presentation.MVP;
using Presentation.Services;
using Presentation.UserInserting;

namespace Presentation.UserList
{
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserInsertingDialogPresenter _userInsertingDialogPresenter;

        public UserListPresenter(IUserRepository userRepository, IUserInsertingDialogPresenter userInsertingDialogPresenter)
        {
            _userRepository = userRepository;
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
        }

        protected override void OnViewLoaded()
        {
            View.InsertingUser += OnInsertingUser;
        }

        private void OnInsertingUser(object sender, EventArgs e)
        {
        }
    }
}
