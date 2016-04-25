using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    public class UserInsertingDialogPresenter : DialogPresenterBase<IUserInsertingDialogView>, IUserInsertingDialogPresenter
    {
        private readonly IUserRepository _userRepository;

        public UserInsertingDialogPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User User { get; private set; }

        public bool? ShowDialog()
        {
            var result = View.ShowDialog();

            if (result == false) 
                return false;
                
            User = View.User;

            return result;
        }
    }
}