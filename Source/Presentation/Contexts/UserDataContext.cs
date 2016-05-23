using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using Model.Annotations;
using Presentation.Validation;

namespace Presentation.Contexts
{
    /// <summary>
    /// �������� ������������� ��� ����� ������ � ������������.
    /// </summary>
    public class UserDataContext : IUserDataContext
    {
        /// <summary>
        /// ��������� ������.
        /// </summary>
        private readonly IValidator _validator;

        /// <summary>
        /// �����, ������������ ����������� ������.
        /// </summary>
        private readonly IPasswordCrypt _passwordCrypt;

        /// <summary>
        /// ������������, � ������� ������ ���� ��������.
        /// </summary>
        private User _user;

        private string _login;

        private string _password;

        private PasswordStrength _passwordStrength;

        private string _surname;

        private string _name;

        private string _lastname;

        private string _position;

        /// <summary>
        /// ������ ����� ������ ������ UserDataContext.
        /// </summary>
        /// <param name="validator">��������� ������.</param>
        /// <param name="passwordCrypt">�����, ������������ ����������� ������.</param>
        public UserDataContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
            _user = new User();
            _passwordStrength = PasswordStrength.PasswordNotSet;
        }

        /// <summary>
        /// ���������� id.
        /// </summary>
        public int Id
        {
            get { return _user.Id; }
        }

        /// <summary>
        /// �������� ��� ���������� �����. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login == value)
                    return;

                var error = _validator.IsLoginValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _login = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������� ��� ���������� ������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                var error = _validator.IsPasswordValid(value);

                if (string.IsNullOrWhiteSpace(error))
                    PasswordStrength = PasswordStrength.PasswordNotSet;

                if (error == ValidatorMessages.LongField)
                    PasswordStrength = PasswordStrength.PasswordNotSet;

                if (error == ValidatorMessages.WeakPassword)
                    PasswordStrength = PasswordStrength.Weak;
                
                if (error == ValidatorMessages.NormalPassword)
                    PasswordStrength = PasswordStrength.Normal;

                if (error == ValidatorMessages.StrongPassword)
                    PasswordStrength = PasswordStrength.Strong;

                _password = value;

                OnPropertyChanged();

                throw new ArgumentException(error);
            }
        }

        /// <summary>
        /// ��������� ��������� ������.
        /// </summary>
        public PasswordStrength PasswordStrength
        {
            get { return _passwordStrength; }
            set
            {
                if (_passwordStrength == value)
                    return;

                _passwordStrength = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������� ��� ���������� �������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname == value)
                    return;

                var error = _validator.IsSurnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _surname = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������� ��� ���������� ���. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;

                var error = _validator.IsNameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _name = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������� ��� ���������� ��������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (_lastname == value)
                    return;

                var error = _validator.IsLastnameValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _lastname = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ���������� ��������.
        /// </summary>
        public string Initials
        {
            get
            {
                return (!string.IsNullOrWhiteSpace(Name) ? (Name.First() + ".") : "") +
                       (!string.IsNullOrWhiteSpace(Lastname) ? (Lastname.First() + ".") : "");
            }
        }

        /// <summary>
        /// �������� ��� ���������� ���������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
        /// </summary>
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position == value)
                    return;

                var error = _validator.IsPositionValid(value);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new ArgumentException(error);

                _position = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������������� �������� ����� �������������.
        /// </summary>
        /// <param name="user">����� ������������, ��������� � ������ ����������.</param>
        public void Initialize(User user)
        {
            _user = user;
            _surname = _user.Surname;
            _name = _user.Name;
            _lastname = _user.Lastname;
            _position = _user.Position;
            _password = _user.Password;
            _login = _user.Login;
        }

        /// <summary>
        /// ���������� ������������, ���������� � ������ ����������.
        /// </summary>
        /// <param name="needHash">����� �� ���������� ��� ������ ������.</param>
        /// <returns></returns>
        public User CreateUser(bool needHash)
        {
            _user.Login = Login;
            _user.Surname = Surname;
            _user.Name = Name;
            _user.Lastname = Lastname;
            _user.Position = Position;
            _user.Initials = Initials;
            _user.Password = needHash ? _passwordCrypt.GetHashString(Password) : Password;

            return _user;
        }

        /// <summary>
        /// �������, ����������� � ���, ��� ���������� �����-�� ��������.�
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// �������� �������� PropertyChanged.
        /// </summary>
        /// <param name="propertyName">��� ��������, ���������� �������.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}