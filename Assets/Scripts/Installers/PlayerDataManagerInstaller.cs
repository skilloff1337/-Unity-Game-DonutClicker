

using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class PlayerDataManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDataManager();
        }

        private void BindDataManager()
        {
            Container
                .Bind<IPlayerDataManager>()
                .To<PlayerDataManager>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }  
    }
}