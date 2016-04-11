using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    public interface IUserInsertingDialogView : IDialogView
    {
        User User { get; }
    }
}