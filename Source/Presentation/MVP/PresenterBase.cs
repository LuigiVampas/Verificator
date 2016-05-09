using System;

namespace Presentation.MVP
{
    /// <summary>
    /// Базовый класс презентера (паттерн MVP).
    /// </summary>
    /// <typeparam name="TView">Вид, связанный с презентером.</typeparam>
    public class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        /// <summary>
        /// Вид, связанный с презентером.
        /// </summary>
        private TView _view;

        /// <summary>
        /// Событие, сообщающее об окончании загрузки вида.ы
        /// </summary>
        public event EventHandler LoadViewCompleted;

        /// <summary>
        /// Возвращает вид, связанный с презентером.
        /// </summary>
        public TView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.LoadCompleted += ViewOnLoadCompleted;
            }
        }

        /// <summary>
        /// Возвращает вид, связанный с презентером.
        /// </summary>
        IView IPresenter.View
        {
            get { return View; }
            set { View = (TView)value; }
        }

        /// <summary>
        /// Инициализирует презентер. Может быть переопределён в дочерних классах.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Запускает презентер.
        /// </summary>
        public void Run()
        {
            View.Show();
        }

        /// <summary>
        /// Обработчик события окончания загрузки вида.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void ViewOnLoadCompleted(object sender, EventArgs e)
        {
            if (LoadViewCompleted != null)
                LoadViewCompleted(this, EventArgs.Empty);

            OnViewLoaded();
        }

        /// <summary>
        /// Пользователь обработчик события. Может быть переопределён в дочерних классах.
        /// </summary>
        protected virtual void OnViewLoaded()
        {
        }
    }
}