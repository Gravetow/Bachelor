using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerInput : ITickable
{
    [Inject] private SignalBus _signalBus;

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            _signalBus.Fire(new BeginDragSignal());
        }

        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            _signalBus.Fire(new DragSignal());
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            _signalBus.Fire(new EndDragSignal());
            _signalBus.Fire(new SubmitSignal());
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button7))
        {
            _signalBus.Fire(new ToggleMenuSignal());
        }
    }
}