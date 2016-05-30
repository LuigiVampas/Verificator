using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Annotations;
using Presentation.Validation;

namespace Presentation.Contexts
{
    /// <summary>
    /// �������� ������������� ��� ��������� ������.
    /// </summary>
    public class PasswordEditContext : IPasswordEditContext
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
        /// ����� �������� ������.
        /// </summary>
        private string _newPassword;

        private PasswordStrength _passwordStrength;

        /// <summary>
        /// ������ ����� ������ ������ PasswordEditContext.
        /// </summary>
        /// <param name="validator">��������� ������.</param>
        /// <param name="passwordCrypt">�����, ������������ ����������� ������.</param>
        public PasswordEditContext(IValidator validator, IPasswordCrypt passwordCrypt)
        {
            _validator = validator;
            _passwordCrypt = passwordCrypt;
            _newPassword = "";
        }
        
        /// <summary>
        /// �������� ��� ���������� ����� ������. ��� ������������ ������ ���������� �������� ������ � ������� IValidator.
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

        public bool PasswordHasError { get; set; }

        /// <summary>
        /// ���������� ��� ������ ������.
        /// </summary>
        /// <returns>��� ������ ������.</returns>
        public string GetNewPasswordHash()
        {
            return _passwordCrypt.GetHashString(_newPassword);
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