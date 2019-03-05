using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class FilterView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private InteractiveSet interactiveSet;

    private List<FilterTag> filterTags = new List<FilterTag>();

    public void SendFilterSignal()
    {
        filterTags.Clear();

        foreach (int indice in interactiveSet.SelectedIndices)
        {
            filterTags.Add((FilterTag)indice);
        }

        _signalBus.Fire(new FilterListSignal() { filterTags = this.filterTags });
    }

    //TODO Remove
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SendFilterSignal();
        }
    }
}