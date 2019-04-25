using UnityEngine;
using Zenject;

public class DetailInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<DetailView>().AsSingle();
    }
}