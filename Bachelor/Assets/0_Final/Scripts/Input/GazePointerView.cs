using UnityEngine;
using Zenject;

public class GazePointerView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;

        _signalBus.Subscribe<SelectSignal>(MoveOnTop);
        _signalBus.Subscribe<DeselectSignal>(MoveBack);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(MoveOnTop);
        _signalBus.Unsubscribe<DeselectSignal>(MoveBack);
    }

    private void MoveOnTop(SelectSignal select)
    {
        transform.position = select.position;
    }

    private void MoveBack()
    {
        transform.localPosition = startPosition;
    }
}