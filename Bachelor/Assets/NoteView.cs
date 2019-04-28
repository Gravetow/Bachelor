using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using TMPro;

[System.Serializable]
public class Note
{
    public string Description;
    public string Title;
}

public class NoteView : MonoBehaviour
{
    //if object selected
    //write title and description
    // attach to data of object
    // show full note in detail panel when selected

    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private ListData listData;

    [SerializeField]
    private GameObject listElementPrefab;

    [SerializeField]
    private Transform listContainer;

    [SerializeField]
    private Keyboard keyboard;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI detailDescription;

    private ListElementData currentlySelectedObjectData;
    private Note currentNote = new Note();

    private void Awake()
    {
        keyboard.OnTextSubmitted += Keyboard_OnTextSubmitted;
        _signalBus.Subscribe<SubmittedSignal>(CreateNoteView);
    }

    private void OnDestroy()
    {
        keyboard.OnTextSubmitted -= Keyboard_OnTextSubmitted;
        _signalBus.Unsubscribe<SubmittedSignal>(CreateNoteView);
    }

    private void CreateNoteView(SubmittedSignal submitted)
    {
        if (submitted.submittedGameObject.GetComponent<MeshRenderer>() == null)
            return;

        foreach (Transform child in listContainer.transform)
        {
            Destroy(child.gameObject);
        }

        currentlySelectedObjectData = listData.listElementData.Find(x => x.Title == submitted.submittedGameObject.name);

        if (currentlySelectedObjectData != null)
        {
            title.SetText(currentlySelectedObjectData.Title);
            detailDescription.SetText(currentlySelectedObjectData.Description);

            foreach (Note listElementData in currentlySelectedObjectData.Notes)
            {
                ListElementView noteElement = Instantiate(listElementPrefab, listContainer).GetComponent<ListElementView>();
                Debug.LogError(listElementData.Description);
                noteElement.SetByNote(listElementData);
            }
        }
    }

    private void Keyboard_OnTextSubmitted(object sender, System.EventArgs e)
    {
        if (currentlySelectedObjectData == null)
            return;

        if (currentNote.Title == null)
        {
            currentNote.Title = keyboard.InputField.text;
            keyboard.gameObject.SetActive(true);
            keyboard.Clear();
        }
        else if (currentNote.Description == null)
        {
            currentNote.Description = keyboard.InputField.text;
            ListElementView listElement = Instantiate(listElementPrefab, listContainer).GetComponent<ListElementView>();
            listElement.SetByNote(currentNote);
            currentlySelectedObjectData.Notes.Add(currentNote);

            keyboard.Close();
        }
    }

    public void ShowKeyboard()
    {
        keyboard.PresentKeyboard();
    }
}