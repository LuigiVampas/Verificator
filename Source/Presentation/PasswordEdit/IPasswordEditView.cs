using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public interface IPasswordEditView : IDialogView
    {
        PasswordEditContext PasswordDataContext { get; set; }
    }
}