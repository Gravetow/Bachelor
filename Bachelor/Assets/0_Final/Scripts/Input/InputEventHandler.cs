using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputEventHandler : IInitializable
{
    [Inject] private SignalBus _signalBus;

    private PointerEventData currentPointerEventData = new PointerEventData(EventSystem.current);

    private GameObject currentlySelectedObject;

    public void Initialize()
    {
        _signalBus.Subscribe<SelectSignal>(TriggerSelect);
        _signalBus.Subscribe<DeselectSignal>(TriggerDeselect);
        _signalBus.Subscribe<SubmitSignal>(TriggerSubmit);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(TriggerSelect);
        _signalBus.Unsubscribe<DeselectSignal>(TriggerDeselect);
        _signalBus.Unsubscribe<SubmitSignal>(TriggerSubmit);
    }

    private void TriggerSelect(SelectSignal select)
    {
        currentlySelectedObject = select.selectedGameObject;
        currentPointerEventData.position = select.position;
        ExecuteEvents.Execute(currentlySelectedObject, currentPointerEventData, ExecuteEvents.pointerEnterHandler);
    }

    private void TriggerDeselect()
    {
        if (currentlySelectedObject == null)
            return;

        ExecuteEvents.Execute(currentlySelectedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        currentlySelectedObject = null;
    }

    private void TriggerSubmit()
    {
        if (currentlySelectedObject == null)
            return;

        ExecuteEvents.Execute(currentlySelectedObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
}