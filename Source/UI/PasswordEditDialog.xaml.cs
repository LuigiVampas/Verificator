using System;
using System.Windows;
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
