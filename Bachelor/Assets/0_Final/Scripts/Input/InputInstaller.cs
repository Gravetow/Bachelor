using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField]
    private MaterialSettings materialSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SelectSignal>();
        Container.DeclareSignal<DeselectSignal>();
        Container.DeclareSignal<SubmitSignal>();

        Container.DeclareSignal<BeginDragSignal>();
        Container.DeclareSignal<DragSignal>();
        Container.DeclareSignal<EndDragSignal>();

        Container.Bind<MaterialSettings>().FromInstance(materialSettings).AsSingle();

        Container.BindInterfacesTo<InputEventHandler>().AsSingle();
        Container.BindInterfacesTo<GazeInput>().AsSingle();
        Container.BindInterfacesTo<ControllerInput>().AsSingle();
        Container.BindInterfacesTo<GazePointerView>().AsSingle();

        Container.BindInterfacesTo<HoverHighlighter>().AsSingle();
    }
}