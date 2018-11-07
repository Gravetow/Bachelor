using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<LookDownSignal>();
        Container.DeclareSignal<LookUpSignal>();
        Container.DeclareSignal<GazeSignal>();
        Container.DeclareSignal<CommitSignal>();

        Container.BindInterfacesTo<GazeInput>().AsSingle();
        Container.BindInterfacesTo<ModeMenu>().AsSingle();
    }
}