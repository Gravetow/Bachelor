using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            meshFilter.gameObject.AddComponent<MeshCollider>();
        }
    }
}