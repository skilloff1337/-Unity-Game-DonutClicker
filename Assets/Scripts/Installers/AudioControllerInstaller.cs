using _6._Audio.Interfaces;
using _6._Audio.Scripts;
using Zenject;

namespace Installers
{
    public class AudioControllerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLineInformation();
        }

        private void BindLineInformation()
        {
            Container
                .Bind<IAudioController>()
                .To<AudioController>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}