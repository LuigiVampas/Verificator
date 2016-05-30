using System;
using System.Diagnostics;
using System.Security;
using System.Windows;
using Data;
using Data.Validation;
using LightInject;
using Presentation;
using Presentation.Contexts;
using Presentation.PasswordEdit;
using Presentation.UserDeleting;
using Presentation.UserEdit;
using Presentation.UserInserting;
using Presentation.UserList;
using Presentation.Validation;
using UI;

namespace Verificator.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Запуск приложения.
        /// </summary>
        /// <param name="e">Аргументы запуска приложения.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            WpfSingleInstance.Make();

            var container = new ServiceContainer();
            container.Register<IUserListPresenter, UserListPresenter>(new PerContainerLifetime());
            container.Register<IMainView>(c => new MainWindow(), new PerContainerLifetime());

            container.Register<IUserInsertingDialogPresenter, UserInsertingDialogPresenter>(new PerContainerLifetime());
            container.Register<IUserInsertingDialogView>(c => new UserInsertingDialog(), new PerContainerLifetime());

            container.Register<IUserDeletingDialogPresenter, UserDeletingDialogPresenter>(new PerContainerLifetime());
            container.Register<IUserDeletingDialogView>(c => new UserDeletingDialog(), new PerContainerLifetime());

            container.Register<IUserEditDialogPresenter, UserEditDialogPresenter>(new PerContainerLifetime());
            container.Register<IUserEditDialogView>(c => new UserEditDialog(), new PerContainerLifetime());

            container.Register<IPasswordEditPresenter, PasswordEditPresenter>(new PerContainerLifetime());
            container.Register<IPasswordEditView>(c => new PasswordEditDialog(), new PerContainerLifetime());

            container.Register<IUserDataContext, UserDataContext>(new PerRequestLifeTime());
            container.Register<Func<IUserDataContext>>(c => (() => c.GetInstance<IUserDataContext>()), new PerContainerLifetime());

            container.Register<IPasswordEditContext, PasswordEditContext>(new PerRequestLifeTime());
            container.Register<Func<IPasswordEditContext>>(c => (() => c.GetInstance<IPasswordEditContext>()));

            container.Register<IUserRepository, DbUserRepository>(new PerContainerLifetime());
            container.Register<IValidator, Validator>(new PerContainerLifetime());
            container.Register<IPasswordCrypt, PasswordCrypt>(new PerContainerLifetime());

            var mainPresenter = container.GetInstance<IUserListPresenter>();
            mainPresenter.Initialize();
            mainPresenter.Run();
        }
    }
}
