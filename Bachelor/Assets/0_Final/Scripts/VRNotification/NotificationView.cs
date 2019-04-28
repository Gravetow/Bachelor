using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class NotificationView : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private AudioSource sound;

    [SerializeField]
    private Image fillImage;

    private void Awake()
    {
        _signalBus.Subscribe<ShowNotificationSignal>(FillText);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ShowNotificationSignal>(FillText);
    }

    public void FillText(ShowNotificationSignal args)
    {
        title.SetText(args.title);
        description.SetText(args.description);

        MoveView();
    }

    private void MoveView()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
        gameObject.transform.position = Camera.main.transform.position - new Vector3(0, 2.35f, 0) + Camera.main.transform.forward * 5;

        gameObject.SetActive(true);

        transform.DOMoveY(transform.position.y + 2, 0.75f).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                sound.Play();
            });
    }

    public void Acknowledge()
    {
        sound.Stop();
        fillImage.DOKill();
        fillImage.fillAmount = 0;
        _signalBus.Fire<AcknowledgeNotificationSignal>();
        transform.DOMoveY(transform.position.y + 12, 3f).SetEase(Ease.InOutQuad).OnComplete(() => gameObject.SetActive(false));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fillImage.DOKill();
        fillImage.fillAmount = 0;
        fillImage.DOFillAmount(1, 5f).OnComplete(() => Acknowledge());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fillImage.DOKill();
        fillImage.fillAmount = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Acknowledge();
    }
}