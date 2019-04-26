using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DataCreater : MonoBehaviour
{
    private int optionNumber = 0;
    private string parentDescription = "";

    public void CreateData()
    {
        foreach (Transform child in transform)
        {
            CreateAssetFromChildren(child);
            optionNumber++;
        }
        AssetDatabase.SaveAssets();
    }

    private void CreateAssetFromChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                ListElementData asset = ScriptableObject.CreateInstance<ListElementData>();
                asset.Title = child.gameObject.name;
                asset.Description = parentDescription;
                asset.Size = child.GetComponent<MeshRenderer>().bounds.max.magnitude;
                asset.FilterTags = new List<FilterTag>();
                asset.FilterTags.Add((FilterTag)optionNumber);

                AssetDatabase.CreateAsset(asset, "Assets/0_Final/Data/sfb/" + asset.Title + ".asset");
            }
            else
            {
                parentDescription += " > " + child.gameObject.name;
                CreateAssetFromChildren(child);
            }
        }
    }
}

[CustomEditor(typeof(DataCreater))]
public class DataCreaterEdtior : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataCreater dataCreater = (DataCreater)target;
        if (GUILayout.Button("Create Data"))
        {
            dataCreater.CreateData();
        }
    }
}