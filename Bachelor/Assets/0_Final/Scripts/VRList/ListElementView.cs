using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ListElementView : MonoBehaviour
{
    private SignalBus _signalBus;

    private ListElementData listElementData;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    public void SetSignalBus(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public string GetTitle()
    {
        return title.text;
    }

    public void SetByNote(Note note)
    {
        title.text = note.Title;
        description.text = note.Description;
    }

    public ListElementData GetData()
    {
        return listElementData;
    }

    public void SetData(ListElementData listElementData)
    {
        this.listElementData = listElementData;

        this.title.SetText(listElementData.Title);
        this.description.SetText(listElementData.Description);
    }

    public List<FilterTag> GetFilterTags()
    {
        return listElementData.FilterTags;
    }

    public float GetAmount()
    {
        return listElementData.Size;
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