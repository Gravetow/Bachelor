using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialSettings", menuName = "Settings/Material", order = 0)]
public class MaterialSettings : ScriptableObject
{
    public Material hoverMaterial;
    public Material highlightMaterial;
}