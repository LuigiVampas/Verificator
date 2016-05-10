using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public class PasswordEditPresenter : DialogPresenterBase<IPasswordEditView>, IPasswordEditPresenter
    {
        public string EditPassword(string oldPasswordHash)
        {
            View.PasswordDataContext = new PasswordEditContext(oldPasswordHash);

            if (View.ShowDialog() == true)
                return View.PasswordDataContext.GetNewPasswordHash();

            return oldPasswordHash;
        }
    }
}