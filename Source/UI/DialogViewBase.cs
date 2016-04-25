using System;
using System.ComponentModel;
using System.Windows;
using Presentation.MVP;

namespace UI
{
    public class DialogViewBase : Window, IDialogView
    {
        public DialogViewBase()
        {
            Loaded += OnWindowLoaded;
            Closing += OnWindowClosing;
        }

        public bool? Result { get; private set; }

        public event EventHandler LoadCompleted;

        public new void Show()
        {
            ShowDialog();
        }

        public new bool? ShowDialog()
        {
            var currentDialog = (Window) this;

            OnShowing();

            currentDialog.ShowDialog();
            return Result;
        }

        protected virtual void OnShowing()
        {
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Result = DialogResult;
            e.Cancel = true;

            OnClosing();

            Hide();
        }

        protected virtual void OnClosing()
        {
            
        }
    }
}