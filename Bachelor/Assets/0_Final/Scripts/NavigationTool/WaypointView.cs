using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaypointView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        _signalBus.Subscribe<SelectSignal>(OnClicked);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(OnClicked);
    }

    public void OnClicked(SelectSignal args)
    {
        if (args.selectedGameObject == gameObject)
        {
            TeleportPlayer();
        }
    }

    public void TeleportPlayer()
    {
        {
            if (target == null)
            {
                target = transform;
            }

            Camera.main.transform.parent.position = target.position;
            Camera.main.transform.parent.localEulerAngles = target.localEulerAngles;

            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }
    }
}