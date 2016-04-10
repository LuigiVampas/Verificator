using System.Windows;
using LightInject;
using Presentation;
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
            container.Register<IMainView, MainWindow>(new PerContainerLifetime());

            var mainPresenter = container.GetInstance<IUserListPresenter>();
            mainPresenter.Initialize();
            mainPresenter.Run();
        }
    }
}
