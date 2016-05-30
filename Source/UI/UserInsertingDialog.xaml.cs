using System;
using System.Windows;
using System.Windows.Controls;
using Presentation.Contexts;
using Presentation.UserInserting;
using Presentation.Validation;

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
        }

        /// <summary>
        /// Возвращает, пользователя с данными, заполненными на диалоговом окне.
        /// </summary>
        public IUserDataContext UserDataContext
        {
            get { return (IUserDataContext)DataContext; }
            set { DataContext = value; }
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
                if (Validation.GetHasError((DependencyObject) child))
                {
                    var errorMessage = Validation.GetErrors((DependencyObject) child)[0].Exception.Message;

                    if (IsNotError(errorMessage)) continue;
                    
                    var control = (UIElement)child;
                    control.Focus();
                    return;
                }
            }
            DialogResult = true;
        }

        public bool IsNotError(string errorMassage)
        {
            return errorMassage == ValidatorMessages.HasNoErrors
                    || errorMassage ==ValidatorMessages.NormalPassword
                    || errorMassage ==ValidatorMessages.StrongPassword;
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
