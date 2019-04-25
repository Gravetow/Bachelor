using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public enum ViewToolMode
{
    OFF, SHOW, HIDE, HIGHLIGHT
}

public class ViewTool : MonoBehaviour
{
    //show list

    [Inject] private SignalBus _signalBus;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private ViewToolMode currentToolMode = ViewToolMode.OFF;

    private void Awake()
    {
        _signalBus.Subscribe<SelectSignal>(ActivateTool);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(ActivateTool);
    }

    public void ActivateShow()
    {
        currentToolMode = ViewToolMode.SHOW;
    }

    public void ActivateHide()
    {
        currentToolMode = ViewToolMode.HIDE;
    }

    public void ActivateHighlight()
    {
        currentToolMode = ViewToolMode.HIGHLIGHT;
    }

    private void ActivateTool(SelectSignal args)
    {
        MeshRenderer meshRenderer = args.selectedGameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            return;

        RaycastHit[] hits;

        switch (currentToolMode)
        {
            case ViewToolMode.OFF:
                break;

            case ViewToolMode.SHOW:

                hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Mathf.Infinity);
                if (hits.Length > 0)
                {
                    hits = hits.OrderBy(hit => hit.distance)
                        .Where(hit => hit.collider.gameObject.GetComponent<MeshRenderer>().enabled == false)
                        .Reverse()
                        .ToArray();

                    if (hits.Length > 0)
                    {
                        hits[0].collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                break;

            case ViewToolMode.HIDE:
                hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Mathf.Infinity);

                if (hits.Length > 0)
                {
                    hits = hits.OrderBy(hit => hit.distance)
                        .Where(hit => hit.collider.gameObject.GetComponent<MeshRenderer>().enabled == true)
                        .ToArray();
                    if (hits.Length > 0)
                    {
                        hits[0].collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
                break;

            case ViewToolMode.HIGHLIGHT:
                meshRenderer.material = meshRenderer.material == defaultMaterial ? defaultMaterial : highlightMaterial;
                break;
        }
    }
}