using System;
using System.Windows;
using System.Windows.Controls;
using Model;
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
            DataContext = new User();
        }

        public User User
        {
            get { return (User)DataContext;  }
            set { DataContext = value; }
        }

        protected override void OnShowing()
        {
            User = new User();
        }

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

        private void UserInsertingDialog_OnActivated(object sender, EventArgs e)
        {
            LoginTextBox.Focus();
        }
    }
}
