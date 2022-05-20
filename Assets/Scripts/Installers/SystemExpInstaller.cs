using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using _7._Level.Interfaces;
using _7._Level.Scripts;
using Zenject;

namespace Installers
{
    public class SystemExpInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystemExp();
        }

        private void BindSystemExp()
        {
            Container
                .Bind<ISystemExp>()
                .To<SystemExp>()
                .AsSingle();
        }
    }
}