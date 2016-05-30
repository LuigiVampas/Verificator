using System.Windows;
using System.Windows.Controls;
using Presentation.Contexts;
using Presentation.PasswordEdit;
using Presentation.Validation;

namespace UI
{
    /// <summary>
    /// Interaction logic for PasswordEditDialog.xaml
    /// </summary>
    public partial class PasswordEditDialog : IPasswordEditView
    {
        /// <summary>
        /// Создаёт новый объект класса PasswordEditDialog.
        /// </summary>
        public PasswordEditDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получает или задаёт контекст представления для изменения пароля.
        /// </summary>
        public IPasswordEditContext PasswordDataContext
        {
            get { return (IPasswordEditContext)DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Обработчик стандартного события нажатия на кнопку OK.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var child in PasswordsGrid.Children)
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
    }
}
