using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GuidingArrowView : MonoBehaviour
{
    [SerializeField]
    private GameObject notification;

    [Inject] private SignalBus _signalBus;

    private MeshRenderer meshRenderer;
    // Use this for initialization

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        _signalBus.Subscribe<ShowNotificationSignal>(Show);
        _signalBus.Subscribe<AcknowledgeNotificationSignal>(Hide);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShowNotificationSignal>(Show);
        _signalBus.Unsubscribe<AcknowledgeNotificationSignal>(Hide);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        meshRenderer.enabled = transform.localEulerAngles.y > 25 && transform.localEulerAngles.y < 335;

        transform.LookAt(notification.transform);
        transform.localEulerAngles = new Vector3(75, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}