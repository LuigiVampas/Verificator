using Presentation.MVP;

namespace Presentation.UserEdit
{
    public interface IUserEditDialogView : IDialogView
    {
        UserDataContext UserDataContext { get; set; }
    }
}