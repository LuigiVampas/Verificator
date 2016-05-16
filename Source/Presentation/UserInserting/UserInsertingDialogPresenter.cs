using System;
using Model;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.UserInserting
{
    /// <summary>
    /// Презентер диалога добавления пользователя.
    /// </summary>
    public class UserInsertingDialogPresenter : DialogPresenterBase<IUserInsertingDialogView>, IUserInsertingDialogPresenter
    {
        private readonly Func<IUserDataContext> _userDataContextFactory;

        public UserInsertingDialogPresenter(Func<IUserDataContext> userDataContextFactory)
        {
            _userDataContextFactory = userDataContextFactory;
        }

        /// <summary>
        /// Возвращает, пользователя с данными, заполненными на диалоговом окне.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Показать диалоговое окно.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        public bool? ShowDialog()
        {
            View.UserDataContext = _userDataContextFactory();

            var result = View.ShowDialog();

            if (result != true) 
                return false;

            User = View.UserDataContext.CreateUser(true);

            return true;
        }
    }
}