using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class DetailPanelView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [Inject] private ListData listData;

    [SerializeField]
    private TextMeshProUGUI description;

    private void Update()
    {
        description.SetText(listData.CurrentDescription);
    }
}