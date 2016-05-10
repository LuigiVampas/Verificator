using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using Model.Annotations;

namespace Presentation
{
    public class UserDataContext : INotifyPropertyChanged
    {
        private User _user;

        public UserDataContext()
        {
            _user = new User();
        }

        public UserDataContext(User user)
        {
            Initialize(user);
        }

        public int Id
        {
            get { return _user.Id; }
        }

        public string Login
        {
            get { return _user.Login; }
            set
            {
                var error = Validator.IsLoginValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Login = value;

                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                var error = Validator.IsPasswordValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Password = value;

                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _user.Surname; }
            set
            {
                var error = Validator.IsSurnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Surname = value;

                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                var error = Validator.IsNameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Name = value;

                OnPropertyChanged();
            }
        }

        public string Lastname
        {
            get { return _user.Lastname; }
            set
            {
                var error = Validator.IsLastnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Lastname = value;

                OnPropertyChanged();
            }
        }

        public string Initials
        {
            get
            {
                var firstNameLetter = Name.First();
                var firstLastnameLetter = Lastname.First();
                return firstNameLetter + "." + firstLastnameLetter + ".";
            }
        }

        public string Position
        {
            get { return _user.Position; }
            set
            {
                var error = Validator.IsPositionValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _user.Position = value;

                OnPropertyChanged();
            }
        }

        public void Initialize(User user)
        {
            _user = (User)user.Clone();
        }

        public User CreateUser(bool needHash)
        {
            var resultUser = (User)_user.Clone();

            if (needHash)
                resultUser.Password = PasswordCrypt.GetHashString(Password);

            return resultUser;
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