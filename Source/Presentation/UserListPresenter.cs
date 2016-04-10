namespace Presentation
{
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        private readonly IUserRepository _userRepository;

        public UserListPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
