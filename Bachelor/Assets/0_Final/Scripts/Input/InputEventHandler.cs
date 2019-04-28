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

        _signalBus.Subscribe<BeginDragSignal>(TriggerBeginDrag);
        _signalBus.Subscribe<DragSignal>(TriggerDrag);
        _signalBus.Subscribe<EndDragSignal>(TriggerEndDrag);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(TriggerSelect);
        _signalBus.Unsubscribe<DeselectSignal>(TriggerDeselect);
        _signalBus.Unsubscribe<SubmitSignal>(TriggerSubmit);

        _signalBus.Unsubscribe<BeginDragSignal>(TriggerBeginDrag);
        _signalBus.Unsubscribe<DragSignal>(TriggerDrag);
        _signalBus.Unsubscribe<EndDragSignal>(TriggerEndDrag);
    }

    private void TriggerSelect(SelectSignal select)
    {
        if (currentlySelectedObject != select.selectedGameObject)
        {
            TriggerDeselect();
            currentlySelectedObject = select.selectedGameObject;
            currentPointerEventData.position = select.position;
            ExecuteEvents.Execute(currentlySelectedObject, currentPointerEventData, ExecuteEvents.pointerEnterHandler);
        }
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
        _signalBus.Fire(new SubmittedSignal() { submittedGameObject = currentlySelectedObject });
    }

    private void TriggerBeginDrag()
    {
        ExecuteEvents.Execute(currentlySelectedObject, currentPointerEventData, ExecuteEvents.beginDragHandler);
    }

    private void TriggerDrag()
    {
        ExecuteEvents.Execute(currentlySelectedObject, currentPointerEventData, ExecuteEvents.dragHandler);
    }

    private void TriggerEndDrag()
    {
        ExecuteEvents.Execute(currentlySelectedObject, currentPointerEventData, ExecuteEvents.endDragHandler);
    }
}