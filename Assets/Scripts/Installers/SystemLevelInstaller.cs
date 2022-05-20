using _7._Level.Interfaces;
using _7._Level.Scripts;
using Zenject;

namespace Installers
{
    public class SystemLevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystemLevel();
        }

        private void BindSystemLevel()
        {
            Container
                .Bind<ISystemLevel>()
                .To<SystemLevel>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}