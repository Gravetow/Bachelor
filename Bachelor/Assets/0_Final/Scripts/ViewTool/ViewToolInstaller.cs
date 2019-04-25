using UnityEngine;
using Zenject;

public class ViewToolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ViewTool>().AsSingle();
    }
}