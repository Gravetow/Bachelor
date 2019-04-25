using UnityEngine;
using Zenject;

public class VRListInstaller : MonoInstaller
{
    public ListData ListData;

    public override void InstallBindings()
    {
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

        Container.BindInterfacesTo<DetailPanelView>().AsSingle();
        Container.BindInterfacesTo<FilterView>().AsSingle();
        //Container.BindInterfacesAndSelfTo<ListView>().AsSingle();
        Container.BindInterfacesTo<ListView>().AsSingle();
    }
}