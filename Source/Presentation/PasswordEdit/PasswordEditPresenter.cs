using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public class PasswordEditPresenter : DialogPresenterBase<IPasswordEditView>, IPasswordEditPresenter
    {
        private readonly Func<IPasswordEditContext> _passwordEditContextFactory;

        public PasswordEditPresenter(Func<IPasswordEditContext> passwordEditContextFactory)
        {
            _passwordEditContextFactory = passwordEditContextFactory;
        }

        public string EditPassword(string oldPassword)
        {
            View.PasswordDataContext = _passwordEditContextFactory();

            if (View.ShowDialog() == true)
                return View.PasswordDataContext.GetNewPasswordHash();

            return oldPassword;
        }
    }
}