using System;

namespace Presentation.MVP
{
    /// <summary>
    /// Интерефес вида (паттерн MVP).
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Событие, сообщающее об окончании загрузки вида.
        /// </summary>
        event EventHandler LoadCompleted;

        /// <summary>
        /// Показать вид.
        /// </summary>
        void Show();

        /// <summary>
        /// Закрыть вид.
        /// </summary>
        void Close();
    }
}