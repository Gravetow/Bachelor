using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FilterSlider : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI text;

    private Slider slider;

    [Inject] private SignalBus _signalBus;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPosition;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out clickPosition))
            return;

        slider.value = (clickPosition.x + 75) / 1.5f;
        _signalBus.Fire(new FilterBySliderSignal() { amount = slider.value });
    }

    private void Update()
    {
        text.text = "Size > " + slider.value;
    }
}