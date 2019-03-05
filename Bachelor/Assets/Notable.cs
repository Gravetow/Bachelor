using HoloToolkit.Unity.InputModule;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Trello;
using HoloToolkit.UI.Keyboard;

public class Notable : MonoBehaviour, IInputClickHandler
{
    public InteractiveMeshCursor cursor;
    public GameObject NoteInput;
    public KeyboardInputField titleInputField;
    public KeyboardInputField descInputField;
    public TrelloSend trelloSend;

    [Inject]
    public ModeSettings settings;

    private string cardName;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (settings.currentMode != Mode.NOTE) return;

        NoteInput.SetActive(true);
    }

    public void SaveToTrello()
    {
        // Create a new Trello card
        var card = new TrelloCard
        {
            name = titleInputField.text,
            desc = descInputField.text
        };

        trelloSend.SendNewCard(card);

        NoteInput.SetActive(false);
    }
}