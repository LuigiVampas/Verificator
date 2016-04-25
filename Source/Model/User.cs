using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Annotations;

namespace Model
{
    public class User : ICloneable, INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _name;
        private string _surname;
        private string _lastname;
        private string _position;

        public User()
        {
            _login = "";
            _password = "";
            _name = "";
            _surname = "";
            _lastname = "";
            _position = "";
        }

        public int Id { get; set; }

        public string Login
        {
            get { return _login; }
            set
            {
                var error = Validator.IsLoginValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Login");

                _login = value;

                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                var error = Validator.IsPasswordValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Password");

                _password = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                var error = Validator.IsSurnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Surname");

                _surname = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                var error = Validator.IsNameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Name");

                _name = value;
            }
        }

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                var error = Validator.IsLastnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Lastname");

                _lastname = value;
            }
        }

        public string Initials
        {
            get
            {
                var firstNameLetter = Name.First();
                var firstSurnameLetter = Surname.First();
                return firstNameLetter + "." + firstSurnameLetter;
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                var error = Validator.IsPasswordValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error, "Position");

                _position = value;
            }
        }

        public object Clone()
        {
            return new User
            {
                _login = _login,
                _password = _password,
                _surname = _surname,
                _name = _name,
                _lastname = _lastname,
                _position = _position
            };
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
