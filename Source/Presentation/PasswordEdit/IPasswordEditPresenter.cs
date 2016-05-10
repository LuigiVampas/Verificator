using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public interface IPasswordEditPresenter : IDialogPresenter<IPasswordEditView>
    {
        string EditPassword(string oldPasswordHash);
    }
}
