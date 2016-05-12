using System;
using Presentation.Contexts;
using Presentation.MVP;

namespace Presentation.PasswordEdit
{
    public class PasswordEditPresenter : DialogPresenterBase<IPasswordEditView>, IPasswordEditPresenter
    {
        private readonly Func<string, IPasswordEditContext> _passwordEditContextFactory;

        public PasswordEditPresenter(Func<string, IPasswordEditContext> passwordEditContextFactory)
        {
            _passwordEditContextFactory = passwordEditContextFactory;
        }

        public string EditPassword(string oldPasswordHash)
        {
            View.PasswordDataContext = _passwordEditContextFactory(oldPasswordHash);

            if (View.ShowDialog() == true)
                return View.PasswordDataContext.GetNewPasswordHash();

            return oldPasswordHash;
        }
    }
}