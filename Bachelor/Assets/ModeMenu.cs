using UnityEngine;
using Zenject;
using DG.Tweening;

public class ModeMenu : MonoBehaviour
{
    [Inject]
    private readonly SignalBus signalBus;

    private Vector3 offset;

    private void Start()
    {
        ShowMenu();
        offset = transform.position;
        signalBus.Subscribe<LookUpSignal>(HideMenu);
        signalBus.Subscribe<LookDownSignal>(ShowMenu);
        signalBus.Subscribe<ButtonPressedSignal>(HideMenu);
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<LookUpSignal>(HideMenu);
        signalBus.Unsubscribe<LookDownSignal>(ShowMenu);
        signalBus.Unsubscribe<ButtonPressedSignal>(HideMenu);
    }

    public void ShowMenu()
    {
        transform.DOScale(1, 0.5f);
    }

    public void HideMenu()
    {
        transform.DOScale(0, 0.5f);
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position + offset;
    }
}