using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuView : MonoBehaviour
{
    [Inject]
    private SignalBus _signalBus;

    private void Awake()
    {
        _signalBus.Subscribe<ToggleMenuSignal>(ToggleMenu);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ToggleMenuSignal>(ToggleMenu);
    }

    private void ToggleMenu()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void NavigationTool(bool openMenu)
    {
        if (openMenu)
        {
            _signalBus.Fire<OpenNavigationToolSignal>();
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

    //TODO: remove
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
        }
    }
}