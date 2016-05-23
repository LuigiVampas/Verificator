using System;
using System.Windows;
using System.Windows.Controls;
using Presentation.Contexts;
using Presentation.UserEdit;

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
                    var control = (UIElement)child;
                    control.Focus();
                    return;
                }
            }
            DialogResult = true;
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
