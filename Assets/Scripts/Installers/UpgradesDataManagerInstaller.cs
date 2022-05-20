using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class UpgradesDataManagerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUpdatesDataManager();
        }

        private void BindUpdatesDataManager()
        {
            Container
                .Bind<IUpgradesDataManager>()
                .To<UpgradesDataManager>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }  
    }
}