using _8._Tooltips.Interfaces;
using _8._Tooltips.Scripts;
using Zenject;

namespace Installers
{
    public class TooltipSystemInstaller  : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTooltipSystem();
        }
        private void BindTooltipSystem()
        {
            Container
                .Bind<ITooltipSystem>()
                .To<TooltipSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        } 
    }
}