using System;
using System.Collections.Generic;
using Model;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserList
{
    /// <summary>
    /// Интерфейс главного вида приложения.
    /// </summary>
    public interface IMainView : IView
    {
        /// <summary>
        /// Список пользователей, отображаемых на экране.
        /// </summary>
        IList<IUserDataContext> Users { get; set; }

        /// <summary>
        /// Выбранный пользователь.
        /// </summary>
        IUserDataContext SelectedUser { get; }

        void ShowErrorMessage(string message);

        /// <summary>
        /// Событие добавления нового пользователя.
        /// </summary>
        event EventHandler InsertingUser;

        /// <summary>
        /// Событие удаления пользователя.
        /// </summary>
        event EventHandler DeletingUser;

        event EventHandler EditingUser;
    }
}