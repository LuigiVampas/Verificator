using System;

namespace Presentation.MVP
{
    /// <summary>
    /// Интерфейс презентера (паттерн MVP).
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Получает вид связанный с презентером.
        /// </summary>
        IView View { get; set; }

        /// <summary>
        /// Запускает презентер.
        /// </summary>
        void Run();

        /// <summary>
        /// Инизиализирует презентер.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Событие, сообщающее о окнчании загрузки вида.
        /// </summary>
        event EventHandler LoadViewCompleted;
    }

    /// <summary>
    /// Интерфейс презентера (паттерн MVP).
    /// </summary>
    /// <typeparam name="TView">Вид, связанный с презентером.</typeparam>
    public interface IPresenter<TView> : IPresenter where TView : IView
    {

        /// <summary>
        /// Получает вид связанный с презентером.
        /// </summary>
        new TView View { get; set; }
    }
}