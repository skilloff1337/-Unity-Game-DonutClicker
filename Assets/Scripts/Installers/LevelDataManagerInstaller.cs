using _4._Donuts.Scripts;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class LevelDataManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDataManager();
        }

        private void BindDataManager()
        {
            Container
                .Bind<ILevelDataManager>()
                .To<LevelDataManager>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }  
    }
}