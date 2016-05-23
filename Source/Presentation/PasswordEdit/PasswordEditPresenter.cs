using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    /// <summary>
    /// ���������, ���������� �� ��������� ������ ������������.
    /// </summary>
    public class PasswordEditPresenter : DialogPresenterBase<IPasswordEditView>, IPasswordEditPresenter
    {
        /// <summary>
        /// ����������� �������, ������������ ����� �������� ������������� ��� ��������� ������.
        /// </summary>
        private readonly Func<IPasswordEditContext> _passwordEditContextFactory;

        /// <summary>
        /// ������ ����� ��������� ������ PasswordEditPresenter.
        /// </summary>
        /// <param name="passwordEditContextFactory">����������� �������, ������������ ����� �������� ������������� ��� ��������� ������.</param>
        public PasswordEditPresenter(Func<IPasswordEditContext> passwordEditContextFactory)
        {
            _passwordEditContextFactory = passwordEditContextFactory;
        }

        /// <summary>
        /// ������ ������ � ������������� � ������ ������. ���������� ��� ������ ������.
        /// </summary>
        /// <param name="oldPassword">������ ������.</param>
        /// <returns>��� ������ ������.</returns>
        public string EditPassword(string oldPassword)
        {
            View.PasswordDataContext = _passwordEditContextFactory();

            if (View.ShowDialog() == true)
                return View.PasswordDataContext.GetNewPasswordHash();

            return oldPassword;
        }
    }
}