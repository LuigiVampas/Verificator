using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    /// <summary>
    /// Презентер, отвечающий за изменение пароля пользователя.
    /// </summary>
    public class PasswordEditPresenter : DialogPresenterBase<IPasswordEditView>, IPasswordEditPresenter
    {
        /// <summary>
        /// Абстрактная фабрика, возвращающая новый контекст представления для изменения пароля.
        /// </summary>
        private readonly Func<IPasswordEditContext> _passwordEditContextFactory;

        /// <summary>
        /// Создаёт новый экземпляр класса PasswordEditPresenter.
        /// </summary>
        /// <param name="passwordEditContextFactory">Абстрактная фабрика, возвращающая новый контекст представления для изменения пароля.</param>
        public PasswordEditPresenter(Func<IPasswordEditContext> passwordEditContextFactory)
        {
            _passwordEditContextFactory = passwordEditContextFactory;
        }

        /// <summary>
        /// Создаёт диалог с пользователем о замене пароля. Возвращает хэш нового пароля.
        /// </summary>
        /// <param name="oldPassword">Старый пароль.</param>
        /// <returns>Хэш нового пароля.</returns>
        public string EditPassword(string oldPassword)
        {
            View.PasswordDataContext = _passwordEditContextFactory();

            if (View.ShowDialog() == true)
                return View.PasswordDataContext.GetNewPasswordHash();

            return oldPassword;
        }
    }
}