using Presentation.MVP;

namespace Presentation.UserInserting
{
    public class UserInsertingDialogPresenter : DialogPresenterBase<IUserInsertingDialogView>, IUserInsertingDialogPresenter
    {
        public bool? ShowDialog()
        {
            return View.ShowDialog();
        }
    }
}