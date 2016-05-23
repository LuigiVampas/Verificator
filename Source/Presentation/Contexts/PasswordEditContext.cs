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

                if (!string.IsNullOrWhiteSpace(error) && error != ValidatorMessages.StrongPassword)
                    throw new ArgumentException(error);

                _newPassword = value;

                OnPropertyChanged();
            }
        }

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