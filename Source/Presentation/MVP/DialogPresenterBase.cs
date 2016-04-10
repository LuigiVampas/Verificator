namespace Presentation.MVP
{
    public class DialogPresenterBase<TView> : PresenterBase<TView>, IDialogPresenter<TView> where TView : IDialogView
    {
        public bool? RunDialog()
        {
            return View.ShowDialog();
        }
    }
}