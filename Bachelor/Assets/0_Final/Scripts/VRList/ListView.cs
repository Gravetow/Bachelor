using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ListView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private ListData listData;

    [SerializeField]
    private GameObject listElementPrefab;

    [SerializeField]
    private Transform listContainer;

    private void Start()
    {
        foreach (ListElementData listElementData in listData.listElementData)
        {
            ListElementView listElement = Instantiate(listElementPrefab, listContainer).GetComponent<ListElementView>();
            listElement.SetSignalBus(_signalBus);
            listElement.SetData(listElementData.Title, listElementData.Description);
        }
    }
}