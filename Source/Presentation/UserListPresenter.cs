using System;

namespace Presentation
{
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        private readonly IUserRepository _userRepository;

        public UserListPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
