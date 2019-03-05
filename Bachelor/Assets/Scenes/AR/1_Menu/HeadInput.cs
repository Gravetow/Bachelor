using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;
using Zenject;

public class HeadInput : MonoBehaviour
{
    [Inject]
    public readonly SignalBus signalBus;

    private bool menuHidden = true;

    private void Start()
    {
        signalBus.Subscribe<ButtonPressedSignal>(Reset);
    }

    private void Reset()
    {
        menuHidden = true;
    }

    private void Update()
    {
        if (Camera.main.transform.localEulerAngles.x > 10 && Camera.main.transform.localEulerAngles.x < 20 && menuHidden)
        {
            signalBus.Fire<LookDownSignal>();
            menuHidden = false;
        }
    }
}