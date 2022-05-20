using _3._UI.Scripts;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerData();
        }

        private void BindPlayerData()
        {
            Container
                .Bind<IPlayerData>()
                .To<PlayerData>()
                .AsSingle();
        }  
    }
}