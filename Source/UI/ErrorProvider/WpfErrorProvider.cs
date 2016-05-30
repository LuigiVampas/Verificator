using System;
using System.Windows;
using Presentation.ErrorProvider;

namespace UI.ErrorProvider
{
    public class WpfErrorProvider : IErrorProvider
    {
        public void ShowDbConnectionErrorMessage(Exception e)
        {
            MessageBox.Show(ErrorMessages.DbConnectionError, ErrorMessages.DbConnectionErrorCaption, MessageBoxButton.OK,
                MessageBoxImage.Error);

            Application.Current.MainWindow.Close();
        }
    }
}
