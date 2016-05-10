using System;
using System.Windows;
using System.Windows.Controls;
using Presentation;
using Presentation.UserInserting;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserInsertingDialog.xaml
    /// </summary>
    public partial class UserInsertingDialog : IUserInsertingDialogView
    {
        /// <summary>
        /// Создаёт диалог добавления пользователя.
        /// </summary>
        public UserInsertingDialog()
        {
            InitializeComponent();

            UserDataContext = new UserDataContext();
        }

        /// <summary>
        /// Возвращает, пользователя с данными, заполненными на диалоговом окне.
        /// </summary>
        public UserDataContext UserDataContext
        {
            get { return (UserDataContext)DataContext; }
            private set { DataContext = value; }
        }

        /// <summary>
        /// Действия, которые необходимо выполнить при показе диалога.
        /// </summary>
        protected override void OnShowing()
        {
            UserDataContext = new UserDataContext();
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку подтверждения добавления нового пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach(var child in UserParametersGrid.Children)
            {
                if (Validation.GetHasError((DependencyObject)child))
                {
                    var control = (UIElement) child;
                    control.Focus();
                    return;
                }
            }
            DialogResult = true;
        }

        /// <summary>
        /// Обработчик стандартного события, возникающего при активизации диалога.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void UserInsertingDialog_OnActivated(object sender, EventArgs e)
        {
            LoginTextBox.Focus();
        }
    }
}
