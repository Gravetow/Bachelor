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

    [SerializeField]
    private float sliderMagnitude = 2f;

    private float currentSliderValue = 0f;
    private List<FilterTag> currentFilterTags = new List<FilterTag>();

    private List<ListElementView> allListElements = new List<ListElementView>();

    private void Awake()
    {
        _signalBus.Subscribe<ShowListSignal>(Show);
        _signalBus.Subscribe<HideListSignal>(Hide);
        _signalBus.Subscribe<FilterListSignal>(SetCurrentFilterTags);
        _signalBus.Subscribe<FilterBySliderSignal>(SetCurrentFilterSliderValue);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShowListSignal>(Show);
        _signalBus.Unsubscribe<HideListSignal>(Hide);
        _signalBus.Unsubscribe<FilterListSignal>(SetCurrentFilterTags);
        _signalBus.Unsubscribe<FilterBySliderSignal>(SetCurrentFilterSliderValue);
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

    public void Filter()
    {
        foreach (ListElementView listElementView in allListElements)
        {
            listElementView.gameObject.SetActive(true);

            if (currentFilterTags.Count > 0)
            {
                foreach (FilterTag filterTag in currentFilterTags)
                {
                    if (listElementView.GetFilterTags().Contains(filterTag) == false)
                    {
                        listElementView.gameObject.SetActive(false);
                        continue;
                    }
                }
            }

            if (listElementView.GetAmount() * sliderMagnitude < currentSliderValue)
            {
                listElementView.gameObject.SetActive(false);
                continue;
            }
        }
    }

    public void SetCurrentFilterTags(FilterListSignal filterListSignal)
    {
        currentFilterTags = filterListSignal.filterTags;
        Filter();
    }

    public void SetCurrentFilterSliderValue(FilterBySliderSignal filterBySliderSignal)
    {
        currentSliderValue = filterBySliderSignal.amount;
        Filter();
    }
}