using _11._Shop.Scripts;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class ClickerSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindClickerSystem();
        }

        private void BindClickerSystem()
        {
            Container
                .Bind<IClickerSystem>()
                .To<ClickerSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}