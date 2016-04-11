using System.Windows;
using Presentation.UserInserting;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserInsertingDialog.xaml
    /// </summary>
    public partial class UserInsertingDialog : IUserInsertingDialogView
    {
        public UserInsertingDialog()
        {
            InitializeComponent();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
