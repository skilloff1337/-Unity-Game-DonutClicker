using _15._Notification.Scripts;
using UnityEngine.Playables;
using Zenject;

namespace Installers
{
    public class NotificationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
                 BindNotificationSystem();
        }

        private void BindNotificationSystem()
        {
            Container
                .Bind<INotificationSystem>()
                .To<NotificationSystem>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}