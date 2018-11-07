using UnityEngine;
using Zenject;
using DG.Tweening;

public class ModeMenu : MonoBehaviour
{
    [Inject]
    private readonly SignalBus signalBus;

    private void Start()
    {
        signalBus.Subscribe<LookDownSignal>(ShowMenu);
        signalBus.Subscribe<LookUpSignal>(HideMenu);
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<LookDownSignal>(ShowMenu);
        signalBus.Unsubscribe<LookUpSignal>(HideMenu);
    }

    public void ShowMenu()
    {
        transform.SetParent(null);
    }

    public void HideMenu()
    {
        transform.SetParent(Camera.main.transform);
    }
}