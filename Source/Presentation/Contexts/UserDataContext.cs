using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using Model.Annotations;

namespace Presentation.Contexts
{
    public class UserDataContext : IUserDataContext
    {
        private readonly IValidator _validator;
        private readonly IPasswordCrypt _passwordCrypt;
        private User _user;

        public UserDataContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
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
                var error = _validator.IsLoginValid(value);

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
                var error = _validator.IsPasswordValid(value);

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
                var error = _validator.IsSurnameValid(value);

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
                var error = _validator.IsNameValid(value);

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
                var error = _validator.IsLastnameValid(value);

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
                var error = _validator.IsPositionValid(value);

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
                resultUser.Password = _passwordCrypt.GetHashString(Password);

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