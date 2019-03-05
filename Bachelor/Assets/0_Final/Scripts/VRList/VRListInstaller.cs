using UnityEngine;
using Zenject;

public class VRListInstaller : MonoInstaller
{
    public ListData ListData;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ShowListSignal>();
        Container.DeclareSignal<SelectListElementSignal>();
        Container.DeclareSignal<FilterListSignal>();
        Container.DeclareSignal<ShowSearchSignal>();
        Container.DeclareSignal<SearchListSignal>();
        Container.DeclareSignal<HideSearchSignal>();
        Container.DeclareSignal<HideListSignal>();

        Container.Bind<ListData>().FromInstance(ListData).AsSingle();

        Container.BindSignal<SelectListElementSignal>()
        .ToMethod<ListData>(x => x.SetCurrentDescription).FromResolve();

        Container.BindInterfacesTo<ListElement>().AsSingle();
        Container.BindInterfacesTo<DetailPanelView>().AsSingle();
    }
}