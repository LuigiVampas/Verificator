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

                if (!string.IsNullOrWhiteSpace(error) && error != ValidatorMessages.StrongPassword)
                    throw new ArgumentException(error);

                _newPassword = value;

                OnPropertyChanged();
            }
        }

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