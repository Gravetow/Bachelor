using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

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

    private ListElementData currentlySelectedObjectData;
    private Note currentNote = new Note();

    private void Awake()
    {
        keyboard.OnTextSubmitted += Keyboard_OnTextSubmitted;
        _signalBus.Subscribe<SelectSignal>(CreateNoteView);
    }

    private void OnDestroy()
    {
        keyboard.OnTextSubmitted -= Keyboard_OnTextSubmitted;
        _signalBus.Unsubscribe<SelectSignal>(CreateNoteView);
    }

    private void CreateNoteView(SelectSignal args)
    {
        if (args.selectedGameObject.GetComponent<MeshRenderer>() == null)
            return;

        foreach (Transform child in listContainer.transform)
        {
            Destroy(child.gameObject);
        }

        currentlySelectedObjectData = listData.listElementData.Find(x => x.Title == args.selectedGameObject.name);

        if (currentlySelectedObjectData != null)
        {
            foreach (Note listElementData in currentlySelectedObjectData.Notes)
            {
                ListElementView noteElement = Instantiate(listElementPrefab, listContainer).GetComponent<ListElementView>();
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
            currentNote.Title = null;
            currentNote.Description = null;

            keyboard.Close();
        }
    }

    // Update is called once per frame
    public void ShowKeyboard()
    {
        keyboard.PresentKeyboard("Enter Note Title");
    }

    public void ApplyKeyboard()
    {
    }
}