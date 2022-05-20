using _0._Localization.Scripts;
using _0._Localization.Scripts.Interfaces;
using _0._Localization.Scripts.Repository;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class EnglishRepositoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEnglishRepository();
        }

        private void BindEnglishRepository()
        {
            Container
                .Bind<ILocalizationRepository>()
                .WithId("English")
                .To<LocalizationEnglishRepository>()
                .AsSingle();
        }
    }
}