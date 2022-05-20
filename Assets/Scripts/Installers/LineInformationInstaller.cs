using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class LineInformationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLineInformation();
        }

        private void BindLineInformation()
        {
            Container
                .Bind<ILineInformation>()
                .To<LineInformation>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}