﻿using System;
using System.Collections.Generic;
using Model;
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
        IList<UserDataContext> Users { get; set; }

        /// <summary>
        /// Выбранный пользователь.
        /// </summary>
        UserDataContext SelectedUser { get; }

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