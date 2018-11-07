using HoloToolkit.Unity.InputModule;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Notable : MonoBehaviour, IInputClickHandler
{
    public InteractiveMeshCursor cursor;
    public GameObject NoteInput;

    [Inject]
    public ModeSettings settings;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (settings.currentMode != Mode.NOTE) return;

        NoteInput.SetActive(true);
    }
}