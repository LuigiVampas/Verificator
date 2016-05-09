using System;
using System.ComponentModel;
using System.Windows;
using Presentation.MVP;

namespace UI
{
    /// <summary>
    /// Базовый класс вида, основанного на диалоге.
    /// </summary>
    public class DialogViewBase : Window, IDialogView
    {
        /// <summary>
        /// Создаёт новый вид, основанный на диалоге.
        /// </summary>
        public DialogViewBase()
        {
            Loaded += OnWindowLoaded;
            Closing += OnWindowClosing;
        }

        /// <summary>
        /// Возвращает результат диалога.
        /// </summary>
        public bool? Result { get; private set; }

        /// <summary>
        /// Событие, сообщающее об окончании загрузки вида.
        /// </summary>
        public event EventHandler LoadCompleted;

        /// <summary>
        /// Показать вид.
        /// </summary>
        public new void Show()
        {
            ShowDialog();
        }

        /// <summary>
        /// Показать диалоговое окно.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        public new bool? ShowDialog()
        {
            var currentDialog = (Window) this;

            OnShowing();

            currentDialog.ShowDialog();

            return Result;
        }

        /// <summary>
        /// Действия, которые необходимо выполнить при показе диалогового окна. 
        /// Дочерние классы могут переопределить этот метод.
        /// </summary>
        protected virtual void OnShowing()
        {
        }

        /// <summary>
        /// Обработчик события загрузки окна.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (LoadCompleted != null)
                LoadCompleted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события закрытия окна.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Result = DialogResult;
            e.Cancel = true;

            OnClosing();

            Hide();
        }

        /// <summary>
        /// Действия, которые необходимо выполнить при закрытии диалогового окна. 
        /// Дочерние классы могут переопределить этот метод.
        /// </summary>
        protected virtual void OnClosing()
        {
            
        }
    }
}