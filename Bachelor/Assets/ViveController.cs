using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{
    private static bool teleportActive;

    private SteamVR_TrackedController viveWand;
    private LineRenderer lineRenderer;
    private bool padTouched;

    public GameObject TeleportIndicator;

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
        if (!teleportActive)
        {
            teleportActive = true;
            padTouched = true;
            TeleportIndicator.SetActive(true);
        }
    }

    private void OnPadUntouched(object sender, ClickedEventArgs e)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        TeleportIndicator.SetActive(false);
        padTouched = false;
        teleportActive = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 currentPosition = viveWand.transform.position;
        Vector3 forwardPosition = viveWand.transform.forward * 50;

        transform.position = currentPosition;
        transform.rotation = viveWand.transform.rotation;

        if (padTouched)
        {
            lineRenderer.SetPosition(0, currentPosition);
            lineRenderer.SetPosition(1, forwardPosition);

            CheckForHit(currentPosition, forwardPosition);
        }
    }

    private bool CheckForHit(Vector3 start, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit))
        {
            TeleportIndicator.transform.position = hit.point;
            TeleportIndicator.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            return true;
        }
        return false;
    }
}