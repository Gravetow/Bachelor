using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ListElementView : MonoBehaviour
{
    private SignalBus _signalBus;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    public void SetSignalBus(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void SetData(string title, string description)
    {
        this.title.SetText(title);
        this.description.SetText(description);
    }

    public void OnSelectElement()
    {
        _signalBus.Fire(new SelectListElementSignal() { description = this.description.text });
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