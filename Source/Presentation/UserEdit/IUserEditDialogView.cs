using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserEdit
{
    /// <summary>
    /// Интерфейс вида, отвечающего за изменение данных пользователя.
    /// </summary>
    public interface IUserEditDialogView : IDialogView
    {
        /// <summary>
        /// Получает или задаёт контекст представления для ввода данных о пользователе.
        /// </summary>
        IUserDataContext UserDataContext { get; set; }

        /// <summary>
        /// Событие, оповещающее о том, что пользователь собирается изменить пароль.
        /// </summary>
        event EventHandler EditPassword;
    }
}