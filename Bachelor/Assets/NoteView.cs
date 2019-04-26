using HoloToolkit.UI.Keyboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NoteView : MonoBehaviour
{
    //if object selected
    //write title and description
    // attach to data of object
    // show full note in detail panel when selected
    // click button for spriteswap
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private ListData listData;

    [SerializeField]
    private GameObject listElementPrefab;

    [SerializeField]
    private Transform listContainer;

    [SerializeField]
    private Keyboard keyboard;

    private List<ListElementView> allListElements = new List<ListElementView>();

    private void Awake()
    {
        keyboard.OnTextSubmitted += Keyboard_OnTextSubmitted;
    }

    private void OnDestroy()
    {
        keyboard.OnTextSubmitted -= Keyboard_OnTextSubmitted;
    }

    private void Keyboard_OnTextSubmitted(object sender, System.EventArgs e)
    {
        Debug.LogError(keyboard.InputField.text);
    }

    // Use this for initialization
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

    // Update is called once per frame
    public void ShowKeyboard()
    {
        keyboard.PresentKeyboard("Enter Note Title");
    }

    public void ApplyKeyboard()
    {
    }
}