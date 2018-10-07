using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{
    private SteamVR_TrackedObject viveWand;

    // Use this for initialization
    private void Start()
    {
        viveWand = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = viveWand.transform.position;
    }
}