using _0._Localization.Interfaces;
using _0._Localization.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalizationSystem();
            BindEnglishRepository();
            BindRussianRepository();
        }
        private void BindLocalizationSystem()
        {
            Debug.Log("Bind Localization System");
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindEnglishRepository()
        {
            Debug.Log("Bind English Repository");
            Container
                .Bind<ILocalizationRepository>()
                .WithId("English")
                .To<LocalizationEnglishRepository>()
                .AsSingle();
        }
        private void BindRussianRepository()
        {
            Debug.Log("Bind Russian Repository");
            Container
                .Bind<ILocalizationRepository>()
                .WithId("Russian")
                .To<LocalizationRussianRepository>()
                .AsSingle();
        }
    }
}