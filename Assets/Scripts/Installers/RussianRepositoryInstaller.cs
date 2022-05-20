using _0._Localization.Scripts;
using _0._Localization.Scripts.Interfaces;
using _0._Localization.Scripts.Repository;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class RussianRepositoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRussianRepository();
        }

        private void BindRussianRepository()
        {
            Container
                .Bind<ILocalizationRepository>()
                .WithId("Russian")
                .To<LocalizationRussianRepository>()
                .AsSingle();
        }
    }
}