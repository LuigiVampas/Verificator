using System;
using System.Collections.Generic;
using Model;
using Presentation.MVP;

namespace Presentation.UserList
{
    public interface IMainView : IView
    {
        IList<User> Users { get; set; }

        event EventHandler InsertingUser;
    }
}