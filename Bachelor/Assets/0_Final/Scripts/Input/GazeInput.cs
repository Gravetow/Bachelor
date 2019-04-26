using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GazeInput : MonoBehaviour
{
    private Camera camera;

    private Vector3 startPosition;
    private GameObject currentlyHitObject;
    private PointerEventData currentPointerEventData = new PointerEventData(EventSystem.current);
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private void Awake()
    {
        //_signalBus.Subscribe<SelectSignal>(Select);

        startPosition = transform.localPosition;
        camera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            currentlyHitObject = hit.collider.gameObject;

            currentPointerEventData = new PointerEventData(EventSystem.current);

            RaycastResult res = new RaycastResult();
            currentPointerEventData.position = hit.point;
            res.gameObject = hit.collider.gameObject;
            ExecuteEvents.Execute(res.gameObject, currentPointerEventData, ExecuteEvents.pointerEnterHandler);
            transform.position = hit.point;
        }
        else
        {
            if (currentlyHitObject != null)
            {
                ExecuteEvents.Execute(currentlyHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                currentlyHitObject = null;
            }

            transform.localPosition = startPosition;
        }

        //TODO:Remove
        if (Input.GetKeyDown(KeyCode.I))
        {
            Select();
        }
    }

    private void Select()
    {
        if (currentlyHitObject != null)
        {
            ExecuteEvents.Execute(currentlyHitObject, currentPointerEventData, ExecuteEvents.pointerClickHandler);
            _signalBus.Fire(new SelectSignal() { selectedGameObject = currentlyHitObject });
        }
    }
}