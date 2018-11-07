using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;
using Zenject;

public class ButtonInput : InteractionReceiver
{
    [Inject]
    public readonly SignalBus signalBus;

    [Inject]
    public ModeSettings modeSettings;

    protected override void InputDown(GameObject obj, InputEventData eventData)
    {
        switch (obj.name)
        {
            case "NavigationButton":
                modeSettings.currentMode = Mode.NAVIGATION;
                break;

            case "ViewButton":
                modeSettings.currentMode = Mode.VIEW;
                break;

            case "NoteButton":
                modeSettings.currentMode = Mode.NOTE;
                break;

            case "EditButton":
                modeSettings.currentMode = Mode.EDIT;
                break;

            case "CreateButton":
                modeSettings.currentMode = Mode.CREATE;
                break;

            case "ResetButton":
                modeSettings.currentMode = Mode.RESET;
                break;
        }
    }
}