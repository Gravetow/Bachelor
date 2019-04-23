using UnityEngine;
using Zenject;

public class NotificationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<ShowNotificationSignal>();
        Container.DeclareSignal<AcknowledgeNotificationSignal>();

        Container.BindInterfacesTo<OverlayView>().AsSingle();
        Container.BindInterfacesTo<NotificationView>().AsSingle();
    }
}