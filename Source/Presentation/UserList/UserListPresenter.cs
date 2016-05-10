using System;
using Presentation.MVP;
using Presentation.UserDeleting;
using Presentation.UserEdit;
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

        private readonly IUserEditDialogPresenter _userEditDialogPresenter;

        /// <summary>
        /// Репозиторий, в котором хранятся объекты класса User.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Создаёт новый главный презентер.
        /// </summary>
        /// <param name="userInsertingDialogPresenter">Презентер диалога добавления нового пользователя.</param>
        /// <param name="userDeletingDialogPresenter">Презентер диалога удаления пользователя.</param>
        /// <param name="userEditDialogPresenter">Презентер диалога изменения данных пользователя.</param>
        /// <param name="userRepository">Репозиторий, в котором хранятся объекты класса User.</param>
        public UserListPresenter(IUserInsertingDialogPresenter userInsertingDialogPresenter, IUserDeletingDialogPresenter userDeletingDialogPresenter, IUserEditDialogPresenter userEditDialogPresenter, IUserRepository userRepository)
        {
            _userInsertingDialogPresenter = userInsertingDialogPresenter;
            _userDeletingDialogPresenter = userDeletingDialogPresenter;
            _userEditDialogPresenter = userEditDialogPresenter;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Обработчик события окончания загрузки вида.
        /// </summary>
        protected override void OnViewLoaded()
        {
            View.InsertingUser += OnInsertingUser;
            View.DeletingUser += OnDeletingUser;
            View.EditingUser += OnEditingUser;
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

                View.Users.Add(new UserDataContext(user));
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
                var selectedUser = View.SelectedUser;
                _userRepository.DeleteUser(selectedUser.CreateUser());
                View.Users.Remove(selectedUser);
            }
        }

        private void OnEditingUser(object sender, EventArgs e)
        {
            if (View.SelectedUser == null)
                return;

            var selectedUser = View.SelectedUser;

            var newUser = _userEditDialogPresenter.EditUser(selectedUser.CreateUser());
        }
    }
}
