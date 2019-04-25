using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DetailView : MonoBehaviour
{
    //show list

    [Inject] private SignalBus _signalBus;

    [SerializeField] private Transform SFBParent;
    [SerializeField] private Transform detailBox;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private LineRenderer lineRenderer;
    private float detailBoxSize;

    private GameObject currentlySelectedGameObject;
    private GameObject detailCopy;

    private void Awake()
    {
        detailBoxSize = detailBox.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        _signalBus.Subscribe<SelectSignal>(CreateDetailView);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(CreateDetailView);
    }

    private void CreateDetailView(SelectSignal args)
    {
        if (args.selectedGameObject.GetComponent<MeshRenderer>() == null)
            return;

        if (currentlySelectedGameObject != null)
        {
            lineRenderer.gameObject.SetActive(false);
            Destroy(detailCopy);
            currentlySelectedGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        }

        currentlySelectedGameObject = args.selectedGameObject;
        args.selectedGameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        detailCopy = Instantiate(args.selectedGameObject, detailBox);
        detailCopy.transform.position = args.selectedGameObject.transform.position;

        detailCopy.transform.DOLocalMove(Vector3.zero, 5f).OnComplete(() =>
        {
            lineRenderer.SetPosition(0, detailBox.transform.position);
            lineRenderer.SetPosition(1, args.selectedGameObject.transform.position);
            lineRenderer.gameObject.SetActive(true);
        });

        ScaleDown();
    }

    private void ScaleDown()
    {
        detailCopy.transform.DOScale(0.5f * detailCopy.transform.localScale, 1f).OnComplete(() =>
        {
            if (GetMaxElement(detailCopy.GetComponent<BoxCollider>().bounds.size) > detailBoxSize)
            {
                ScaleDown();
            }
        });
    }

    private float GetMaxElement(Vector3 vector)
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }
}