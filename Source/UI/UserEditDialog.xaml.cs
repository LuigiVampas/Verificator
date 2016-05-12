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
        public UserEditDialog()
        {
            InitializeComponent();
        }

        public IUserDataContext UserDataContext
        {
            get { return (IUserDataContext) DataContext; }
            set { DataContext = value; }
        }

        public event EventHandler EditPassword;

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

        private void PasswordEditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (EditPassword != null)
                EditPassword(this, EventArgs.Empty);
        }
    }
}
