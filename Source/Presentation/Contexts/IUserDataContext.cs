using System.ComponentModel;
using Model;

namespace Presentation.Contexts
{
    public interface IUserDataContext : INotifyPropertyChanged
    {
        int Id { get; }

        string Login { get; set; }

        string Password { get; set; }

        string Surname { get; set; }

        string Name { get; set; }

        string Lastname { get; set; }

        string Initials { get;  }

        string Position { get; set; }

        void Initialize(User user);

        User CreateUser(bool needHash);
    }
}