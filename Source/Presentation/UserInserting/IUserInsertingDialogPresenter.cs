using Model;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    public interface IUserInsertingDialogPresenter : IDialogPresenter<IUserInsertingDialogView>
    {
        User User { get; }

        bool? ShowDialog();
    }
}