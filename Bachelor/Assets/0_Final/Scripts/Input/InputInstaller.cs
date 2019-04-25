using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SelectSignal>();

        Container.BindInterfacesTo<GazeInput>().AsSingle();
    }
}