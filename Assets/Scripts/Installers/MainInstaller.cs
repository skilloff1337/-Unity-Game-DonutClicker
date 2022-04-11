using _0._Localization.Interfaces;
using _0._Localization.Scripts;
using _0._Localization.Scripts.Repository;
using _1._Logs.Scripts;
using _1._Logs.Scripts.Interfaces;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
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
            BindLogSystem();
            BindLineInformation();
            BindDonutConvertSystem();
            BindPlayerData();
        }
        private void BindLocalizationSystem()
        {
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindEnglishRepository()
        {
            Container
                .Bind<ILocalizationRepository>()
                .WithId("English")
                .To<LocalizationEnglishRepository>()
                .AsSingle();
        }
        private void BindRussianRepository()
        {
            Container
                .Bind<ILocalizationRepository>()
                .WithId("Russian")
                .To<LocalizationRussianRepository>()
                .AsSingle();
        }

        private void BindLogSystem()
        {
            Container
                .Bind<ILogSystem>()
                .To<LogSystem>()
                .AsSingle();
        }
        
        private void BindLineInformation()
        {
            Container
                .Bind<ILineInformation>()
                .To<LineInformation>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindDonutConvertSystem()
        {
            Container
                .Bind<IDonutConvertSystem>()
                .To<DonutConvertSystem>()
                .AsSingle();
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