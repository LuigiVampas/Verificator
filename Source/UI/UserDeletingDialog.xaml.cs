using System.Windows;
using Presentation.UserDeleting;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserDeletingDialog.xaml
    /// </summary>
    public partial class UserDeletingDialog : IUserDeletingDialogView
    {
        public UserDeletingDialog()
        {
            InitializeComponent();
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
