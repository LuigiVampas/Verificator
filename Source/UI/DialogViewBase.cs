using System;
using System.Linq;
using System.Windows;
using Presentation.MVP;

namespace UI
{
    public class DialogViewBase : Window, IDialogView
    {
        public DialogViewBase()
        {
            Loaded += OnWindowLoaded;
        }

        public bool? Result { get; private set; }

        public event EventHandler LoadCompleted;

        public new void Show()
        {
            ShowDialog();
        }

        public new bool? ShowDialog()
        {
            var activeWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(w => w.IsActive);
            if (activeWindow == null) throw new ApplicationException("No active window found");

            Result = activeWindow.ShowDialog();
            return Result;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }
    }
}