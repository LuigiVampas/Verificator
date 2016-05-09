using System.Windows;
using Presentation.UserDeleting;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserDeletingDialog.xaml
    /// </summary>
    public partial class UserDeletingDialog : IUserDeletingDialogView
    {
        /// <summary>
        /// Создаёт диалог удаления пользователя.
        /// </summary>
        public UserDeletingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку подтверждения удаления пользователя.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
