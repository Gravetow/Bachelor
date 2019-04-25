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

        //Debug.Log(EventSystem.current.currentSelectedGameObject);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != currentlyHitObject)
            {
                currentlyHitObject = hit.collider.gameObject;

                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

                RaycastResult res = new RaycastResult();
                pointerEventData.position = hit.point;
                res.gameObject = hit.collider.gameObject;
                ExecuteEvents.Execute(res.gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler);
            }
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
            ExecuteEvents.Execute(currentlyHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            _signalBus.Fire(new SelectSignal() { selectedGameObject = currentlyHitObject });
        }
    }
}