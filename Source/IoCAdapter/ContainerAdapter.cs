using System;
using LightInject;
using Presentation.MVP;

namespace IoCAdapter
{
    public class ContainerAdapter
    {
        private ServiceContainer _innerContainer;

        public ContainerAdapter()
        {
            _innerContainer = new ServiceContainer();
        }

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            _innerContainer.Register<TService, TImplementation>();
        }

        public void RegisterDialogView(Func<IDialogView> factory)
        {
            
        }
    }
}
