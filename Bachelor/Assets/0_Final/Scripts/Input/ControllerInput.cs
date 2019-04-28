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
            _signalBus.Fire(new SubmitSignal());
        }
    }
}