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

        private readonly IActiveWindowProvider _activeWindowProvider;

        public UserListPresenter(IUserRepository userRepository, IUserInsertingDialogPresenter userInsertingDialogPresenter, IActiveWindowProvider activeWindowProvider)
        {
            _userRepository = userRepository;
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
            _activeWindowProvider = activeWindowProvider;
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
