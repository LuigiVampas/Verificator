using System;

namespace Presentation.MVP
{
    public interface IPresenter
    {
        IView View { get; set; }

        void Run();

        void Initialize();

        event EventHandler LoadViewCompleted;
    }

    public interface IPresenter<TView> : IPresenter where TView : IView
    {
        new TView View { get; set; }
    }
}