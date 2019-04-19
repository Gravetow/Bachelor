using UnityEngine;
using Zenject;

public class VRNotificationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<ShowNotificationSignal>();
        Container.DeclareSignal<AcknowledgeNotificationSignal>();
    }
}