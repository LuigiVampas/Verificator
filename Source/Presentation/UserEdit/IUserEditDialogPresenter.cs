using Model;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    public interface IUserEditDialogPresenter : IDialogPresenter<IUserEditDialogView>
    {
        User EditUser(User editingUser);
    }
}