using System.Windows;
using LightInject;
using Presentation.UserDeleting;
using Presentation.UserInserting;
using Presentation.UserList;
using UI;

namespace Verificator.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new ServiceContainer();
            container.Register<IUserListPresenter, UserListPresenter>(new PerContainerLifetime());
            container.Register<IMainView>(c => new MainWindow(), new PerContainerLifetime());
            container.Register<IUserInsertingDialogPresenter, UserInsertingDialogPresenter>(new PerContainerLifetime());
            container.Register<IUserInsertingDialogView>(c => new UserInsertingDialog(), new PerContainerLifetime());
            container.Register<IUserDeletingDialogPresenter, UserDeletingDialogPresenter>(new PerContainerLifetime());
            container.Register<IUserDeletingDialogView>(c => new UserDeletingDialog(), new PerContainerLifetime());

            var mainPresenter = container.GetInstance<IUserListPresenter>();
            mainPresenter.Initialize();
            mainPresenter.Run();
        }
    }
}
