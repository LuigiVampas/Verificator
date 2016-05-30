using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Annotations;
using Presentation.Validation;

namespace Presentation.Contexts
{
    /// <summary>
    /// Контекст представления для изменения пароля.
    /// </summary>
    public class PasswordEditContext : IPasswordEditContext
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
        /// Новый введённый пароль.
        /// </summary>
        private string _newPassword;

        private PasswordStrength _passwordStrength;

        /// <summary>
        /// Создаёт новый объект класса PasswordEditContext.
        /// </summary>
        /// <param name="validator">Валидатор данных.</param>
        /// <param name="passwordCrypt">Класс, занимающийся шифрованием пароля.</param>
        public PasswordEditContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
            _newPassword = "";
        }
        
        /// <summary>
        /// Получает или возвращает новый пароль. При установлении пароля происходит проверка данных с помощью IValidator.
        /// </summary>
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword == value)
                    return;

                var error = _validator.IsPasswordValid(value);

                if (string.IsNullOrWhiteSpace(error))
                {
                    PasswordStrength = PasswordStrength.PasswordNotSet;
                    PasswordHasError = true;
                }

                if (error == ValidatorMessages.LongField)
                {
                    PasswordStrength = PasswordStrength.PasswordNotSet;
                    PasswordHasError = true;
                }

                if (error == ValidatorMessages.WeakPassword)
                {
                    PasswordStrength = PasswordStrength.Weak;
                    PasswordHasError = true;
                }

                if (error == ValidatorMessages.NormalPassword)
                {
                    PasswordStrength = PasswordStrength.Normal;
                    PasswordHasError = false;
                }

                if (error == ValidatorMessages.StrongPassword)
                {
                    PasswordStrength = PasswordStrength.Strong;
                    PasswordHasError = false;
                }

                _newPassword = value;

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

        public bool PasswordHasError { get; set; }

        /// <summary>
        /// Возвращает хэш нового пароля.
        /// </summary>
        /// <returns>Хэш нового пароля.</returns>
        public string GetNewPasswordHash()
        {
            return _passwordCrypt.GetHashString(_newPassword);
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