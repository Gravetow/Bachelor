using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SelectSignal>();
        Container.DeclareSignal<DeselectSignal>();
        Container.DeclareSignal<SubmitSignal>();

        Container.BindInterfacesTo<InputEventHandler>().AsSingle();
        Container.BindInterfacesTo<GazeInput>().AsSingle();
        Container.BindInterfacesTo<ControllerInput>().AsSingle();
        Container.BindInterfacesTo<GazePointerView>().AsSingle();
    }
}