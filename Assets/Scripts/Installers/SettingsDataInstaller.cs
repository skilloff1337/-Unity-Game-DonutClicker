using _0._Localization.Scripts;
using _0._Localization.Scripts.Repository;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class SettingsDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSettingsData();
        }

        private void BindSettingsData()
        {
            Container
                .Bind<ISettingsData>()
                .To<SettingsData>()
                .AsSingle();
        }
    }
}