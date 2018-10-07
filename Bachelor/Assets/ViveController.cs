using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{
    private static bool teleportActive;

    private SteamVR_TrackedController viveWand;
    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private bool padTouched;
    public Vector3 targetTeleportPosition;

    private MeshRenderer currentMesh;

    public GameObject TeleportIndicator;

    // Use this for initialization
    private void Start()
    {
        viveWand = GetComponent<SteamVR_TrackedController>();
        lineRenderer = GetComponent<LineRenderer>();

        viveWand.PadTouched += OnPadTouched;
        viveWand.PadUntouched += OnPadUntouched;
        viveWand.PadClicked += OnPadClicked;
    }

    private void OnDestroy()
    {
        viveWand.PadTouched -= OnPadTouched;
        viveWand.PadUntouched -= OnPadUntouched;
        viveWand.PadUntouched -= OnPadClicked;
    }

    private void OnPadClicked(object sender, ClickedEventArgs e)
    {
        transform.parent.position = targetTeleportPosition;
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
    }

    private void LateUpdate()
    {
        Vector3 currentPosition = viveWand.transform.position;
        Vector3 forwardPosition = viveWand.transform.forward * 50;
        if (padTouched)
        {
            lineRenderer.SetPosition(0, currentPosition);

            CheckForHit(currentPosition, forwardPosition);
        }
    }

    private bool CheckForHit(Vector3 start, Vector3 direction)
    {
        if (Physics.Raycast(start, direction, out hit))
        {
            TeleportIndicator.transform.position = hit.point;
            TeleportIndicator.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.GetType() == typeof(BoxCollider))
            {
                if (currentMesh == null)
                {
                    currentMesh = hit.collider.GetComponent<MeshRenderer>();
                    currentMesh.enabled = true;
                }
                else if (currentMesh.gameObject != hit.collider.gameObject)
                {
                    currentMesh.enabled = false;
                    currentMesh = hit.collider.GetComponent<MeshRenderer>();
                    targetTeleportPosition = currentMesh.transform.position;
                    currentMesh.enabled = true;
                }
            }
            else
            {
                targetTeleportPosition = hit.point;
            }
            return true;
        }
        lineRenderer.SetPosition(1, direction);

        return false;
    }
}