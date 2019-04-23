using UnityEngine;
using Zenject;

public class OverlayView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private GameObject overviewCanvas;

    private void Awake()
    {
        _signalBus.Subscribe<ShowNotificationSignal>(Show);
        _signalBus.Subscribe<AcknowledgeNotificationSignal>(Hide);

        overviewCanvas.SetActive(true);
        overviewCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShowNotificationSignal>(Show);
        _signalBus.Unsubscribe<AcknowledgeNotificationSignal>(Hide);
    }

    private void Show()
    {
        overviewCanvas.SetActive(true);
    }

    private void Hide()
    {
        overviewCanvas.SetActive(false);
    }

    //TODO: remove
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _signalBus.Fire(new ShowNotificationSignal()
            {
                title = "Notification",
                description = "The changes for the 3D-model have been saved and the application will now visually represent them. " + "\n" +
                "Please wait for a second before applying any new filters."
            });
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _signalBus.Fire<AcknowledgeNotificationSignal>();
        }
    }
}