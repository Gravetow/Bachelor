using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DetailView : MonoBehaviour
{
    //show list
    //add line
    //tween gameobject to List + resize gameobject

    [Inject] private SignalBus _signalBus;

    [SerializeField] private Transform SFBParent;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Vector3 offsetSFB = new Vector3(740, 0, 0);

    private void Awake()
    {
        _signalBus.Subscribe<SelectSignal>(CreateDetailView);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(CreateDetailView);
    }

    private void CreateDetailView(SelectSignal args)
    {
        args.selectedGameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        GameObject detailCopy = Instantiate(args.selectedGameObject, args.selectedGameObject.transform);

        detailCopy.transform.DOMove(Camera.main.transform.position += offsetSFB, 10f);
    }
}