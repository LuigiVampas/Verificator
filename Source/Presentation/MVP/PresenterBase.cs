using System;

namespace Presentation.MVP
{
    public class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        private TView _view;

        public event EventHandler LoadViewCompleted;

        public TView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.LoadCompleted += ViewOnLoadCompleted;
            }
        }

        IView IPresenter.View
        {
            get { return View; }
            set { View = (TView)value; }
        }

        public virtual void Initialize()
        {
        }

        public void Run()
        {
            View.Show();
        }

        private void ViewOnLoadCompleted(object sender, EventArgs e)
        {
            if (LoadViewCompleted != null)
                LoadViewCompleted(this, EventArgs.Empty);

            OnViewLoaded();
        }

        protected virtual void OnViewLoaded()
        {
        }
    }
}