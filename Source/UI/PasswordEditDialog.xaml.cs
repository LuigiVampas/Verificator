using System.Windows;
using System.Windows.Controls;
using Presentation.Contexts;
using Presentation.PasswordEdit;

namespace UI
{
    /// <summary>
    /// Interaction logic for PasswordEditDialog.xaml
    /// </summary>
    public partial class PasswordEditDialog : IPasswordEditView
    {
        public PasswordEditDialog()
        {
            InitializeComponent();
        }

        public IPasswordEditContext PasswordDataContext
        {
            get { return (IPasswordEditContext)DataContext; }
            set { DataContext = value; }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var child in PasswordsGrid.Children)
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
