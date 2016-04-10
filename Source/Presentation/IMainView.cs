using System;
using System.Collections.Generic;
using Model;

namespace Presentation
{
    public interface IMainView : IView
    {
        IList<User> Users { get; set; }

        event EventHandler InsertingUser;
    }
}