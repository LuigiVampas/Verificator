using System;
using System.Collections.Generic;
using Model;
using Presentation.MVP;

namespace Presentation.UserList
{
    public interface IMainView : IView
    {
        IList<User> Users { get; set; }

        User SelectedUser { get; }

        event EventHandler InsertingUser;

        event EventHandler DeletingUser;
    }
}