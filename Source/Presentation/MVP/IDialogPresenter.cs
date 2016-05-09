namespace Presentation.MVP
{
    /// <summary>
    /// Интерфейс презентера, основанного на диалоге.   
    /// </summary>
    /// <typeparam name="TView">Вид, связанный с презентером.</typeparam>
    public interface IDialogPresenter<TView> : IPresenter<TView> where TView : IDialogView
    {
        /// <summary>
        /// Запускает диалог.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        bool? RunDialog();
    }
}