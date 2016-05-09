namespace Presentation.MVP
{
    /// <summary>
    /// Ѕазовый класс презентера, основанного на диалоге.
    /// </summary>
    /// <typeparam name="TView">¬ид, св€занный с презентером.</typeparam>
    public class DialogPresenterBase<TView> :  PresenterBase<TView>, IDialogPresenter<TView> where TView : IDialogView
    {
        /// <summary>
        /// «апускает диалог.
        /// </summary>
        /// <returns>true - если пользователь OK, false - в противном случае.</returns>
        public bool? RunDialog()
        {
            return View.ShowDialog();
        }
    }
}