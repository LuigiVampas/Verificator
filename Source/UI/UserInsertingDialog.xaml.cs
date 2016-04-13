using System.Windows;
using Model;
using Presentation.UserInserting;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserInsertingDialog.xaml
    /// </summary>
    public partial class UserInsertingDialog : IUserInsertingDialogView
    {
        private readonly User _user;

        public UserInsertingDialog()
        {
            InitializeComponent();
            _user = new User();
        }

        public User User
        {
            get
            {
                _user.Login = LoginTextBox.Text;
                _user.Password = PasswordTextBox.Text;
                _user.Surname = SurnameTextBox.Text;
                _user.Name = NameTextBox.Text;
                _user.LastName = LastNameTextBox.Text;
                _user.Position = PositionTextBox.Text;

                return _user;
            }

        }

        protected override void OnClosing()
        {
            LoginTextBox.Text = "";
            PasswordTextBox.Text = "";
            SurnameTextBox.Text = "";
            NameTextBox.Text = "";
            LastNameTextBox.Text = "";
            PositionTextBox.Text = "";
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
