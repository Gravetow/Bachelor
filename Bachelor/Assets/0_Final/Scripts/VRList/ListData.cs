using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListData", menuName = "VRList", order = 0)]
public class ListData : ScriptableObject
{
    public string CurrentDescription;
    public List<ListElementData> listElementData;

    public void SetCurrentDescription(SelectListElementSignal selectListElementSignal)
    {
        CurrentDescription = "" + selectListElementSignal.Id;
    }
}