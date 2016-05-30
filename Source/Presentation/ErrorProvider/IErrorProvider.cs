using System;

namespace Presentation.ErrorProvider
{
    public interface IErrorProvider
    {
        void ShowDbConnectionErrorMessage(Exception e);
    }
}
