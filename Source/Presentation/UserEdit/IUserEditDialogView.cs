using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    public interface IUserEditDialogView : IDialogView
    {
        IUserDataContext UserDataContext { get; set; }

        event EventHandler EditPassword;
    }
}