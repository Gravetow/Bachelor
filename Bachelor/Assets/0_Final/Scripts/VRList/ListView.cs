using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ListView : MonoBehaviour
{
    [Inject] private ListData listData;

    public GameObject ListElementPrefab;
    public Transform ListContainer;

    private void Start()
    {
        foreach (ListElementData listElementData in listData.listElementData)
        {
            ListElement listElement = Instantiate(ListElementPrefab, ListContainer).GetComponent<ListElement>();
        }
    }
}