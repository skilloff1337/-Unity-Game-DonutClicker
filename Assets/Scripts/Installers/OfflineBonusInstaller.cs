using System.ComponentModel;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class OfflineBonusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindOfflineBonus();
        }

        private void BindOfflineBonus()
        {
            Container
                .Bind<IOfflineBonus>()
                .To<OfflineBonus>()
                .AsSingle();
        }  
    }
}