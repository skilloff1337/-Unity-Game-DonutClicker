using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using Zenject;

namespace Installers
{
    public class StatisticsDataManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDonutConvertManager();
        }

        private void BindDonutConvertManager()
        {
            Container
                .Bind<IStatisticsDataManager>()
                .To<StatisticsDataManager>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }  
    }
}