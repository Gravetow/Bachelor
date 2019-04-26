using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapView : MonoBehaviour
{
    [SerializeField]
    private List<WaypointView> waypoints = new List<WaypointView>();

    private int i = 0;

    public void PreviousWaypoint()
    {
        if (i < 0)
        {
            i = waypoints.Count - 1;
        }

        waypoints[i].TeleportPlayer();
    }

    public void NextWaypoint()
    {
        if (i >= waypoints.Count)
        {
            i = 0;
        }
        waypoints[i].TeleportPlayer();
    }
}