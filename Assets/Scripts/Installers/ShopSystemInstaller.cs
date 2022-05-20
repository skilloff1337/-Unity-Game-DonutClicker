using _11._Shop.Scripts;
using Zenject;

namespace Installers
{
    public class ShopSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindShopSystem();
        }

        private void BindShopSystem()
        {
            Container
                .Bind<IShopSystem>()
                .To<ShopSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}