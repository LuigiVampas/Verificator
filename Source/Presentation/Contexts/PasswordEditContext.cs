using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Annotations;
using Presentation.Validation;

namespace Presentation.Contexts
{
    public class PasswordEditContext : IPasswordEditContext
    {
        private readonly IValidator _validator;
        private readonly IPasswordCrypt _passwordCrypt;
        private string _newPassword;

        public PasswordEditContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
            _newPassword = "";
        }
        
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword == value)
                    return;

                var error = _validator.IsPasswordValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _newPassword = value;

                OnPropertyChanged();
            }
        }

        public string GetNewPasswordHash()
        {
            return _passwordCrypt.GetHashString(_newPassword);
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