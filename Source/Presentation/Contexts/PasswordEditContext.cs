using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using Model.Annotations;

namespace Presentation.Contexts
{
    public class PasswordEditContext : INotifyPropertyChanged
    {
        private string _oldPassword;
        private string _newPassword;

        public PasswordEditContext(string oldPasswordHash)
        {
            OldPasswordHashSum = oldPasswordHash;
            _oldPassword = "";
            _newPassword = "";
        }

        public string OldPasswordHashSum { get; private set; }

        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                if (_oldPassword == value)
                    return;

                _oldPassword = value;

                if (!PasswordCrypt.IsPasswordValid(_oldPassword, OldPasswordHashSum))
                    throw new ArgumentException("Неправильный пароль!");

                OnPropertyChanged();
            }
        }
        
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword == value)
                    return;

                var error = Validator.IsPasswordValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _newPassword = value;

                OnPropertyChanged();
            }
        }

        public string GetNewPasswordHash()
        {
            return PasswordCrypt.GetHashString(_newPassword);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}