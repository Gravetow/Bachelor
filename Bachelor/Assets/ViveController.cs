using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{
    private SteamVR_TrackedController viveWand;
    private LineRenderer lineRenderer;
    private bool padTouched;

    // Use this for initialization
    private void Start()
    {
        viveWand = GetComponent<SteamVR_TrackedController>();
        lineRenderer = GetComponent<LineRenderer>();

        viveWand.PadTouched += OnPadTouched;
        viveWand.PadUntouched += OnPadUntouched;
    }

    private void OnDestroy()
    {
        viveWand.PadTouched -= OnPadTouched;
        viveWand.PadUntouched -= OnPadUntouched;
    }

    private void OnPadTouched(object sender, ClickedEventArgs e)
    {
        padTouched = true;
    }

    private void OnPadUntouched(object sender, ClickedEventArgs e)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        padTouched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = viveWand.transform.position;
        transform.rotation = viveWand.transform.rotation;

        if (padTouched)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.forward * 10);
        }
    }
}