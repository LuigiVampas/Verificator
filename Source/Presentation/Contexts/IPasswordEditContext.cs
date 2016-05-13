using System.ComponentModel;

namespace Presentation.Contexts
{
    public interface IPasswordEditContext : INotifyPropertyChanged
    {
        string NewPassword { get; set; }

        string GetNewPasswordHash();
    }
}