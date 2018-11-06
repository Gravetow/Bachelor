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
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<LookDownSignal>(ShowMenu);
    }

    public void ShowMenu()
    {
        Debug.Log("AHA");
        canvasGroup.DOFade(1, 0.5f);
    }

    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}