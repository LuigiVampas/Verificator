using System;
using Presentation.MVP;
using Presentation.UserDeleting;
using Presentation.UserInserting;

namespace Presentation.UserList
{
    /// <summary>
    /// Главный презентер приложения.
    /// </summary>
    public class UserListPresenter : PresenterBase<IMainView>, IUserListPresenter
    {
        /// <summary>
        /// Презентер диалога добавления нового пользователя.
        /// </summary>
        private readonly IUserInsertingDialogPresenter _userInsertingDialogPresenter;

        /// <summary>
        /// Презентер диалога удаления пользователя.
        /// </summary>
        private readonly IUserDeletingDialogPresenter _userDeletingDialogPresenter;

        /// <summary>
        /// Репозиторий, в котором хранятся объекты класса User.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Создаёт новый главный презентер.
        /// </summary>
        /// <param name="userInsertingDialogPresenter">Презентер диалога добавления нового пользователя.</param>
        /// <param name="userDeletingDialogPresenter">Презентер диалога удаления пользователя.</param>
        /// <param name="userRepository">Репозиторий, в котором хранятся объекты класса User.</param>
        public UserListPresenter(IUserInsertingDialogPresenter userInsertingDialogPresenter, IUserDeletingDialogPresenter userDeletingDialogPresenter, IUserRepository userRepository)
        {
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
            _userDeletingDialogPresenter = userDeletingDialogPresenter;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обработчик события окончания загрузки вида.
        /// </summary>
        protected override void OnViewLoaded()
        {
            View.InsertingUser += OnInsertingUser;
            View.DeletingUser += OnDeletingUser;
        }

        /// <summary>
        /// Обработчик события добавления нового пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnInsertingUser(object sender, EventArgs e)
        {
            var okButtonPressed = _userInsertingDialogPresenter.ShowDialog() == true;

            if (okButtonPressed)
            {
                var user = _userInsertingDialogPresenter.User;
                _userRepository.AddUser(user);

                View.Users.Add(user);
            }
        }

        /// <summary>
        /// Обработчик события удаления пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnDeletingUser(object sender, EventArgs e)
        {
            if (View.SelectedUser == null)
                return;

            if (_userDeletingDialogPresenter.RunDialog() == true)
            {
                var activeUser = View.SelectedUser;
                _userRepository.DeleteUser(activeUser);
                View.Users.Remove(activeUser);
            }
        }
    }
}
