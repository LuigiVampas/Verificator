using System;

namespace Presentation.MVP
{
    public interface IView
    {
        event EventHandler LoadCompleted;

        void Show();

        void Close();
    }
}