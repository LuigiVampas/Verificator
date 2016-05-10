using System.Windows;
using System.Windows.Controls;
using Presentation;
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

        public UserDataContext UserDataContext
        {
            get { return (UserDataContext) DataContext; }
            set { DataContext = value; }
        }

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
    }
}
