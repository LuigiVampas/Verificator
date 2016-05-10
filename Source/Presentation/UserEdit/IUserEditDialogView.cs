using Model;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    public interface IUserEditDialogView : IDialogView
    {
        User EditingUser { get; set; }
    }
}