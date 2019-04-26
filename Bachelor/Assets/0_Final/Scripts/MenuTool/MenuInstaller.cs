using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<OpenNavigationToolSignal>();
        Container.DeclareSignal<CloseNavigationToolSignal>();
        Container.DeclareSignal<OpenNoteToolSignal>();
        Container.DeclareSignal<CloseNoteToolSignal>();
        Container.DeclareSignal<CreateWaypointSignal>();
        Container.DeclareSignal<OpenViewToolSignal>();
        Container.DeclareSignal<CloseViewToolSignal>();
        Container.DeclareSignal<OpenDetailToolSignal>();
        Container.DeclareSignal<CloseDetailToolSignal>();
        Container.DeclareSignal<OpenManipulationToolSignal>();
        Container.DeclareSignal<CloseManipulationToolSignal>();

        Container.BindInterfacesTo<MenuView>().AsSingle();
    }
}