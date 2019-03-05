using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ListElement : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;
    private int id = 0;

    public void OnSelectElement()
    {
        _signalBus.Fire(new SelectListElementSignal() { Id = id });
    }

    //TODO Remove
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnSelectElement();
        }
    }
}