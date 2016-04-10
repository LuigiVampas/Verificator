using System;

namespace Presentation
{
    public interface IView
    {
        event EventHandler LoadCompleted;

        void Show();

        void Close();
    }
}