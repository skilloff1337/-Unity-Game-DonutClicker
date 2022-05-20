using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class DonutConvertSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDonutConvertSystem();
        }

        private void BindDonutConvertSystem()
        {
            Container
                .Bind<IDonutConvertSystem>()
                .To<DonutConvertSystem>()
                .AsSingle();
        }  
    }
}