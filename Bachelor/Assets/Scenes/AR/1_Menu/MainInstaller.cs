using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ButtonPressedSignal>();
        Container.DeclareSignal<LookUpSignal>();
        Container.DeclareSignal<GazeSignal>();
        Container.DeclareSignal<CommitSignal>();

        Container.Bind<ModeSettings>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesTo<ButtonInput>().AsSingle();
        Container.BindInterfacesTo<ModeMenu>().AsSingle();
    }
}