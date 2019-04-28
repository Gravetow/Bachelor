using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MinimapView : MonoBehaviour
{
    [SerializeField]
    private List<WaypointView> waypoints = new List<WaypointView>();

    private int i = 0;

    [SerializeField]
    private GameObject targetModel;

    [Inject] private SignalBus _signalBus;

    private void Awake()
    {
        _signalBus.Subscribe<CreateWaypointSignal>(AddWaypointToList);
        _signalBus.Subscribe<OpenNavigationToolSignal>(ActivateMinimap);
        _signalBus.Subscribe<CloseNavigationToolSignal>(DeactivateMinimap);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<CreateWaypointSignal>(AddWaypointToList);
        _signalBus.Unsubscribe<OpenNavigationToolSignal>(ActivateMinimap);
        _signalBus.Unsubscribe<CloseNavigationToolSignal>(DeactivateMinimap);
    }

    private void ActivateMinimap()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        transform.LookAt(Camera.main.transform);

        gameObject.SetActive(true);

        foreach (BoxCollider collider in targetModel.GetComponentsInChildren<BoxCollider>())
        {
            collider.enabled = false;
        }
    }

    private void DeactivateMinimap()
    {
        gameObject.SetActive(false);
        foreach (BoxCollider collider in targetModel.GetComponentsInChildren<BoxCollider>())
        {
            collider.enabled = true;
        }
    }

    private void AddWaypointToList(CreateWaypointSignal args)
    {
        GameObject newWaypoint = Instantiate(waypoints[0].gameObject);
        newWaypoint.transform.position = Camera.main.transform.position;
        newWaypoint.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);
        waypoints.Add(newWaypoint.GetComponent<WaypointView>());
    }

    public void PreviousWaypoint()
    {
        i--;

        if (i < 0)
        {
            i = waypoints.Count - 1;
        }

        Teleport();
    }

    public void NextWaypoint()
    {
        i++;
        if (i >= waypoints.Count)
        {
            i = 0;
        }

        Teleport();
    }

    public void Teleport()
    {
        waypoints[i].TeleportPlayer();
        transform.position = Camera.main.transform.position + new Vector3(0, -0.5f, 2f); ;
    }
}