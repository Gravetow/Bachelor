using UnityEngine;
using Zenject;
using DG.Tweening;

public class ModeMenu : MonoBehaviour
{
    [Inject]
    private readonly SignalBus signalBus;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0;
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
        canvasGroup.DOFade(1, 0.5f);
    }

    public void HideMenu()
    {
        transform.SetParent(Camera.main.transform);

        canvasGroup.DOFade(0, 0.5f).OnComplete(ResetMenu);
    }

    public void ResetMenu()
    {
        transform.localPosition = new Vector3(0, 0, 2);
        transform.localEulerAngles = new Vector3(45, 0, 0);
    }
}