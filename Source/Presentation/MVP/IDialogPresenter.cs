namespace Presentation.MVP
{
    public interface IDialogPresenter<TView> : IPresenter<TView> where TView : IDialogView
    {
        bool? RunDialog();
    }
    public class DialogPresenterBase<TView> : PresenterBase<TView>, IDialogPresenter<TView> where TView : IDialogView
    {
        public bool? RunDialog()
        {
            throw new System.NotImplementedException();
        }
    }
}