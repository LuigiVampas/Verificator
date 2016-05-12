using System.ComponentModel;

namespace Presentation.Contexts
{
    public interface IPasswordEditContext : INotifyPropertyChanged
    {
        string OldPasswordHashSum { get;  }

        string OldPassword { get; set; }

        string NewPassword { get; set; }

        string GetNewPasswordHash();
    }
}