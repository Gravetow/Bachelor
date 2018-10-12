using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private SteamVR_TrackedController viveWand;
    public GameObject NotePrefab;

    private GameObject attachedGameobject;

    private GameObject currentNote;

    // Use this for initialization
    private void Start()
    {
        viveWand = GetComponent<SteamVR_TrackedController>();

        viveWand.TriggerClicked += OnTriggerClicked;
        viveWand.TriggerUnclicked += OnTriggerUnclicked;
    }

    private void OnDestroy()
    {
        viveWand.TriggerClicked -= OnTriggerClicked;
        viveWand.TriggerUnclicked -= OnTriggerUnclicked;
    }

    public void OnTriggerClicked(object sender, ClickedEventArgs e)
    {
        currentNote = Instantiate(NotePrefab, gameObject.transform);
    }

    public void OnTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (attachedGameobject == null)
        {
            Destroy(currentNote);
        }
        else
        {
            currentNote.transform.SetParent(attachedGameobject.transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        attachedGameobject = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        attachedGameobject = null; ;
    }
}