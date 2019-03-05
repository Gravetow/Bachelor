using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListElementData", menuName = "VRList/ListElementData", order = 0)]
public class ListElementData : ScriptableObject
{
    private int id;
    public string Title;
    public string Description;
}