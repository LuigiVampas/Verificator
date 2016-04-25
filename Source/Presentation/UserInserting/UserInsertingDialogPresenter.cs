using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    public class UserInsertingDialogPresenter : DialogPresenterBase<IUserInsertingDialogView>, IUserInsertingDialogPresenter
    {
        public User User { get; private set; }

        public bool? ShowDialog()
        {
            var result = View.ShowDialog();

            if (result == false) 
                return false;
                
            User = (User)View.User.Clone();

            return result;
        }
    }
}