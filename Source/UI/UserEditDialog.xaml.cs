using System;
using System.Windows;
using System.Windows.Controls;
using Presentation.Contexts;
using Presentation.UserEdit;
using Presentation.Validation;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserEditDialog.xaml
    /// </summary>
    public partial class UserEditDialog : IUserEditDialogView
    {
        /// <summary>
        /// Создаёт новый объект класса UserEditDialog.
        /// </summary>
        public UserEditDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получает или задаёт контекст представления для ввода данных о пользователе.
        /// </summary>
        public IUserDataContext UserDataContext
        {
            get { return (IUserDataContext) DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Событие, оповещающее о том, что пользователь собирается изменить пароль.
        /// </summary>
        public event EventHandler EditPassword;

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку ОК.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var child in UserParametersGrid.Children)
            {
                if (Validation.GetHasError((DependencyObject)child))
                {
                    var errorMessage = Validation.GetErrors((DependencyObject)child)[0].Exception.Message;

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
                    || errorMassage == ValidatorMessages.NormalPassword
                    || errorMassage == ValidatorMessages.StrongPassword;
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку изменения пароля.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void PasswordEditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (EditPassword != null)
                EditPassword(this, EventArgs.Empty);
        }
    }
}
