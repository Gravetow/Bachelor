using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FilterToggle : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<InteractiveToggle>().OnInputClicked(new HoloToolkit.Unity.InputModule.InputClickedEventData(EventSystem.current));
    }
}