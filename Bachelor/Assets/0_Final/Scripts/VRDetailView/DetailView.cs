using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DetailView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private Transform detailBox;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private LineRenderer lineRenderer;
    private float detailBoxSize;

    private GameObject detailCopy;

    private void Awake()
    {
        detailBoxSize = detailBox.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        _signalBus.Subscribe<SubmittedSignal>(CreateDetailView);
        _signalBus.Subscribe<OpenDetailToolSignal>(OpenDetailView);
        _signalBus.Subscribe<CloseDetailToolSignal>(CloseDetailView);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SubmittedSignal>(CreateDetailView);
        _signalBus.Unsubscribe<OpenDetailToolSignal>(OpenDetailView);
        _signalBus.Unsubscribe<CloseDetailToolSignal>(CloseDetailView);
    }

    private void OpenDetailView()
    {
        gameObject.SetActive(true);
    }

    private void CloseDetailView()
    {
        gameObject.SetActive(false);
    }

    private void CreateDetailView(SubmittedSignal submitted)
    {
        if (submitted.submittedGameObject.GetComponent<MeshRenderer>() == null)
            return;

        if (detailCopy != null)
        {
            lineRenderer.gameObject.SetActive(false);
            Destroy(detailCopy);
            submitted.submittedGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        }

        submitted.submittedGameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        detailCopy = Instantiate(submitted.submittedGameObject, detailBox);
        Vector3 startPosition = submitted.submittedGameObject.transform.position;

        detailCopy.transform.position = submitted.submittedGameObject.transform.position;

        detailCopy.transform.DOLocalMove(Vector3.zero, 5f).OnComplete(() =>
        {
            lineRenderer.SetPosition(0, detailBox.transform.position);
            lineRenderer.SetPosition(1, startPosition);
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
            else
            {
                detailCopy.GetComponent<BoxCollider>().enabled = false;
            }
        });
    }

    private float GetMaxElement(Vector3 vector)
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }
}