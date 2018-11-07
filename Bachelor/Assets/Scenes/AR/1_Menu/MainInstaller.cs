using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ButtonPressedSignal>();
        Container.DeclareSignal<LookUpSignal>();
        Container.DeclareSignal<LookDownSignal>();
        Container.DeclareSignal<GazeSignal>();
        Container.DeclareSignal<CommitSignal>();

        Container.Bind<ModeSettings>().AsSingle();
        Container.BindInterfacesTo<Zoomable>().AsSingle();
        Container.BindInterfacesTo<ButtonInput>().AsSingle();
        Container.BindInterfacesTo<HeadInput>().AsSingle();
        Container.BindInterfacesTo<ModeMenu>().AsSingle();
    }
}