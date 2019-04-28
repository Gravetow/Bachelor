using UnityEngine;
using Zenject;

public class NavigationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<PreviousWaypointSignal>();
        Container.DeclareSignal<NextWaypointSignal>();

        Container.BindInterfacesTo<WaypointView>().AsSingle();
        Container.BindInterfacesTo<MinimapView>().AsSingle();
    }
}