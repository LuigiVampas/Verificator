using System;
using Model;
using Presentation.Contexts;
using Presentation.MVP;
using Presentation.PasswordEdit;

namespace Presentation.UserEdit
{
    /// <summary>
    /// Презентер, отвечающий за изменение данных пользователя.
    /// </summary>
    public class UserEditDialogPresenter : DialogPresenterBase<IUserEditDialogView>, IUserEditDialogPresenter
    {
        /// <summary>
        /// Презентер, отвечающий за изменение пароля пользователя.
        /// </summary>
        private readonly IPasswordEditPresenter _passwordEditPresenter;

        /// <summary>
        /// Абстрактная фабрика, возвращающая новый контекст представления для ввода данных о пользователе.
        /// </summary>
        private readonly Func<IUserDataContext> _userDataContextFactory;

        /// <summary>
        /// Создаёт новый объект класса UserEditDialogPresenter.
        /// </summary>
        /// <param name="passwordEditPresenter">Презентер, отвечающий за изменение пароля пользователя.</param>
        /// <param name="userDataContextFactory">Абстрактная фабрика, возвращающая новый контекст представления для ввода данных о пользователе.</param>
        public UserEditDialogPresenter(IPasswordEditPresenter passwordEditPresenter, Func<IUserDataContext> userDataContextFactory)
        {
            _passwordEditPresenter = passwordEditPresenter;
            _userDataContextFactory = userDataContextFactory;
        }

        /// <summary>
        /// Действия, которые необходимо выполнить после загрузки вида.
        /// </summary>
        protected override void OnViewLoaded()
        {
            View.EditPassword += OnEditPassword;
        }

        /// <summary>
        /// Запуспить диалог.
        /// </summary>
        /// <returns>Результат диалога.</returns>
        public new bool RunDialog()
        {
            return false;
        }

        /// <summary>
        /// Создаёт диалог с пользователем об изменении данных пользователя. Возвращает измененного пользователя.
        /// </summary>
        /// <param name="editingUser">Пользователь, данные которого нужно изменить.</param>
        /// <returns>Возвращает изменённого пользователя.</returns>
        public User EditUser(User editingUser)
        {
            var dataContext = _userDataContextFactory();
            dataContext.Initialize(editingUser);

            View.UserDataContext = dataContext;

            if (View.ShowDialog() == true)
            {
                var resultUser = View.UserDataContext.CreateUser(false);

                if (resultUser == null)
                    return null;

                editingUser.Name = resultUser.Name;
                editingUser.Surname = resultUser.Surname;
                editingUser.Password = resultUser.Password;
                editingUser.Lastname = resultUser.Lastname;
                editingUser.Position = resultUser.Position;
            }

            return editingUser;
        }

        /// <summary>
        /// Действия, которые необходимо выполнить при получении оповещения о том, что пользователь собирается изменить пароль.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnEditPassword(object sender, EventArgs e)
        {
            var user = View.UserDataContext.CreateUser(false);
            user.Password = _passwordEditPresenter.EditPassword(user.Password);
            View.UserDataContext.Initialize(user);
        }
    }
}