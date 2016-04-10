namespace Presentation.MVP
{
    public interface IDialogPresenter<TView> : IPresenter<TView> where TView : IDialogView
    {
        bool? RunDialog();
    }
}