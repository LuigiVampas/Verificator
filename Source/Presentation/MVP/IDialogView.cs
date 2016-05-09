namespace Presentation.MVP
{
    /// <summary>
    /// Вид диалога.
    /// </summary>
    public interface IDialogView : IView
    {
        /// <summary>
        /// Возвращает результат диалога.
        /// </summary>
        bool? Result { get; }

        /// <summary>
        /// Показать диалоговое окно.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        bool? ShowDialog();
    }
}