using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    private static bool teleportActive;

    private SteamVR_TrackedController viveWand;
    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private bool padTouched;
    public Vector3 targetTeleportPosition;

    private BuildingLooker lastBuilding;
    private BuildingLooker currentBuilding;

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

        if (currentBuilding != null)
        {
            currentBuilding.GetComponent<BoxCollider>().enabled = false;
        }

        if (lastBuilding != null)
        {
            lastBuilding.GetComponent<BoxCollider>().enabled = true;
        }
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
        if (currentBuilding != null)
        {
            foreach (GameObject wall in currentBuilding.Walls)
            {
                wall.SetActive(true);
            }
            currentBuilding = null;
        }

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
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.GetType() == typeof(BoxCollider))
            {
                if (currentBuilding == null)
                {
                    currentBuilding = hit.collider.GetComponent<BuildingLooker>();
                    foreach (GameObject wall in currentBuilding.Walls)
                    {
                        wall.SetActive(false);
                    }
                    targetTeleportPosition = currentBuilding.Waypoint.transform.position;
                }
                else if (currentBuilding.gameObject != hit.collider.gameObject)
                {
                    foreach (GameObject wall in currentBuilding.Walls)
                    {
                        wall.SetActive(true);
                    }
                    lastBuilding = currentBuilding;

                    currentBuilding = hit.collider.GetComponent<BuildingLooker>();
                    foreach (GameObject wall in currentBuilding.Walls)
                    {
                        wall.SetActive(false);
                    }

                    targetTeleportPosition = currentBuilding.Waypoint.transform.position;
                }
            }
            else if (hit.collider.GetType() == typeof(CapsuleCollider))
            {
                targetTeleportPosition = hit.point;
            }
            return true;
        }
        targetTeleportPosition = transform.parent.position;
        lineRenderer.SetPosition(1, direction);

        return false;
    }
}