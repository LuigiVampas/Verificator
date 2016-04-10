using System.Windows;
using LightInject;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new ServiceContainer();
        }
    }
}
