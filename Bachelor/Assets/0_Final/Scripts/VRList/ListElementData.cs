using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListElementData", menuName = "VRList/ListElementData", order = 0)]
public class ListElementData : ScriptableObject
{
    public string Title;
    public string Description;
    public float Size;
    public List<FilterTag> FilterTags;
    public List<Note> Notes;
}

public enum FilterTag
{
    FIRSTOPTION,
    SECONDOPTION,
    THIRDOPTION
}