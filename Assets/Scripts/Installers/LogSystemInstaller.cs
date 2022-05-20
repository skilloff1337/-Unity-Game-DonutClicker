using _0._Localization.Scripts;
using _1._Logs.Scripts;
using _1._Logs.Scripts.Interfaces;
using Zenject;

namespace Installers
{
    public class LogSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLogSystem();
        }

        private void BindLogSystem()
        {
            Container
                .Bind<ILogSystem>()
                .To<LogSystem>()
                .AsSingle();
        }
    }
}