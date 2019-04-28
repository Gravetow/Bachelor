using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaypointView : MonoBehaviour
{
    [Inject] public SignalBus _signalBus;

    [SerializeField]
    private Transform target;

    private void Start()
    {
        _signalBus.Subscribe<SubmittedSignal>(OnClicked);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SubmittedSignal>(OnClicked);
    }

    public void OnClicked(SubmittedSignal submitted)
    {
        if (submitted.submittedGameObject == gameObject)
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