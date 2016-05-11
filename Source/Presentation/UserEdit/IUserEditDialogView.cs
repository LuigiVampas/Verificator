﻿using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    public interface IUserEditDialogView : IDialogView
    {
        UserDataContext UserDataContext { get; set; }

        event EventHandler EditPassword;
    }
}