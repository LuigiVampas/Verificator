using System;

namespace Presentation.MVP
{
    public interface IPresenter
    {
        IView View { get; set; }

        void Run();

        event EventHandler LoadViewCompleted;
    }

    public interface IPresenter<TView> : IPresenter where TView : IView
    {
        new TView View { get; set; }

        void Initialize();
    }

    public interface IDialogPresenter<TView> : IPresenter<TView> where TView : IDialogView
}