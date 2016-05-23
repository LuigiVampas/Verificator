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
    /// Контекст представления для ввода данных о пользователе.
    /// </summary>
    public class UserDataContext : IUserDataContext
    {
        /// <summary>
        /// Валидатор данных.
        /// </summary>
        private readonly IValidator _validator;

        /// <summary>
        /// Класс, занимающийся шифрованием пароля.
        /// </summary>
        private readonly IPasswordCrypt _passwordCrypt;

        /// <summary>
        /// Пользователь, с которым связан этот контекст.
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
        /// Создаёт новый объект класса UserDataContext.
        /// </summary>
        /// <param name="validator">Валидатор данных.</param>
        /// <param name="passwordCrypt">Класс, занимающийся шифрованием пароля.</param>
        public UserDataContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
            _user = new User();
            _passwordStrength = PasswordStrength.PasswordNotSet;
        }

        /// <summary>
        /// Возвращает id.
        /// </summary>
        public int Id
        {
            get { return _user.Id; }
        }

        /// <summary>
        /// Получает или возвращает логин. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Получает или возвращает пароль. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Сложность введённого пароля.
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
        /// Получает или возвращает фамилию. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Получает или возвращает имя. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Получает или возвращает отчество. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Возвращает инициалы.
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
        /// Получает или возвращает должность. При установлении пароля происходит проверка данных с помощью IValidator.
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
        /// Инициализирует контекст новый пользователем.
        /// </summary>
        /// <param name="user">Новый пользователь, связанный с данным контекстом.</param>
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
        /// Возвращает пользователя, связанного с данным контекстом.
        /// </summary>
        /// <param name="needHash">Нужно ли возвращать хэш вместо пароля.</param>
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
        /// Событие, оповещающее о том, что изменилось какое-то свойство.ы
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает свойство PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Имя свойства, вызвавшего событие.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}