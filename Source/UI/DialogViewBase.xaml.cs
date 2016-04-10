using System;
using System.Linq;
using System.Windows;
using Presentation.MVP;

namespace UI
{
    /// <summary>
    /// Interaction logic for DialogViewBase.xaml
    /// </summary>
    public partial class DialogViewBase : IDialogView
    {
        public DialogViewBase()
        {
            InitializeComponent();
        }

        public bool? Result { get; private set; }

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
    }
}
