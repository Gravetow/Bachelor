using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuView : MonoBehaviour
{
    [Inject]
    private SignalBus _signalBus;

    public void NavigationTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenNavigationToolSignal>();
            Debug.LogError("aha");
        }
        else
        {
            _signalBus.Fire<CloseNavigationToolSignal>();
        }
    }

    public void NoteTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenNoteToolSignal>();
        }
        else
        {
            _signalBus.Fire<CloseNoteToolSignal>();
        }
    }

    public void CreateWaypoint()
    {
        _signalBus.Fire<CreateWaypointSignal>();
    }

    public void ViewTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenViewToolSignal>();
        }
        else
        {
            _signalBus.Fire<CloseViewToolSignal>();
        }
    }

    public void ManipulationTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenManipulationToolSignal>();
        }
        else
        {
            _signalBus.Fire<CloseManipulationToolSignal>();
        }
    }

    public void DetailTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenDetailToolSignal>();
        }
        else
        {
            _signalBus.Fire<CloseDetailToolSignal>();
        }
    }
}