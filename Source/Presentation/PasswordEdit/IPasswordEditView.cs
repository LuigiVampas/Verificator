using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public interface IPasswordEditView : IDialogView
    {
        IPasswordEditContext PasswordDataContext { get; set; }
    }
}