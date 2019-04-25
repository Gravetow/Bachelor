using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DetailView : MonoBehaviour
{
    //show list
    //add line

    [Inject] private SignalBus _signalBus;

    [SerializeField] private Transform SFBParent;
    [SerializeField] private Transform detailBox;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private LineRenderer lineRenderer;
    private float detailBoxSize;

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
        args.selectedGameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        GameObject detailCopy = Instantiate(args.selectedGameObject, detailBox);
        detailCopy.transform.position = args.selectedGameObject.transform.position;

        detailCopy.transform.DOLocalMove(Vector3.zero, 5f);
        ScaleDown(detailCopy);
    }

    private void ScaleDown(GameObject target)
    {
        target.transform.DOScale(0.5f * target.transform.localScale, 1f).OnComplete(() =>
        {
            if (GetMaxElement(target.GetComponent<BoxCollider>().bounds.size) > detailBoxSize)
            {
                ScaleDown(target);
            }
        });
    }

    private float GetMaxElement(Vector3 vector)
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }
}