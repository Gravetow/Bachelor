using HoloToolkit.Unity.InputModule;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Trello;

public class Notable : MonoBehaviour, IInputClickHandler
{
    public InteractiveMeshCursor cursor;
    public GameObject NoteInput;

    public TrelloSend trelloSend;

    [Inject]
    public ModeSettings settings;

    [Inject]
    public readonly SignalBus signalBus;

    private string cardName;

    private void Start()
    {
        signalBus.Subscribe<ButtonPressedSignal>(ShowNote);
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<ButtonPressedSignal>(ShowNote);
    }

    public void ShowNote()
    {
        if (settings.currentMode != Mode.NOTE) return;

        NoteInput.SetActive(true);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //if (settings.currentMode != Mode.NOTE) return;

        //NoteInput.SetActive(true);
    }

    public void SaveCardName(string title)
    {
        cardName = title;
    }

    public void SaveToTrello(string description)
    {
        // Create a new Trello card
        var card = new TrelloCard
        {
            name = "test",
            desc = "description"
        };

        trelloSend.SendNewCard(card);
    }
}