using _1._Logs.Scripts;
using _1._Logs.Scripts.Interfaces;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class MediatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMediator();
        }

        private void BindMediator()
        {
            Container
                .Bind<IMediator>()
                .To<Mediator>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }  
    }
}