using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ListView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private ListData listData;

    [SerializeField]
    private GameObject listElementPrefab;

    [SerializeField]
    private Transform listContainer;

    private List<ListElementView> allListElements = new List<ListElementView>();

    private void Awake()
    {
        _signalBus.Subscribe<ShowListSignal>(Show);
        _signalBus.Subscribe<HideListSignal>(Hide);
        _signalBus.Subscribe<FilterListSignal>(Filter);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShowListSignal>(Show);
        _signalBus.Unsubscribe<HideListSignal>(Hide);
        _signalBus.Unsubscribe<FilterListSignal>(Filter);
    }

    private void Start()
    {
        foreach (ListElementData listElementData in listData.listElementData)
        {
            ListElementView listElement = Instantiate(listElementPrefab, listContainer).GetComponent<ListElementView>();
            allListElements.Add(listElement);
            listElement.SetSignalBus(_signalBus);
            listElement.SetData(listElementData);
        }
    }

    public void Show()
    {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = new Vector3(0, 0, 6);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.SetParent(null);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Filter(FilterListSignal filterListSignal)
    {
        Debug.Log(allListElements.Count);
        foreach (ListElementView listElementView in allListElements)
        {
            foreach (FilterTag filterTag in filterListSignal.filterTags)
            {
                if (listElementView.GetFilterTags().Contains(filterTag) == false)
                {
                    listElementView.gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    listElementView.gameObject.SetActive(true);
                }
            }
        }
    }
}