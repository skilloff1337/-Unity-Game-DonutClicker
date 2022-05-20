using _0._Localization.Scripts;
using _0._Localization.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class LocalizationSystemInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalizationSystem();
        }

        private void BindLocalizationSystem()
        {
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}