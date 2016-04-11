using System.Windows;
using LightInject;
using Presentation;
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

            var mainPresenter = container.GetInstance<IUserListPresenter>();
            mainPresenter.Initialize();
            mainPresenter.Run();
        }
    }
}
