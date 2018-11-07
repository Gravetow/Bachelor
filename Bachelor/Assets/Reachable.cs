using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class Reachable : MonoBehaviour, IInputHandler
{
    public InteractiveMeshCursor cursor;

    [Inject]
    public ModeSettings settings;

    private bool reaching;

    public void OnInputDown(InputEventData eventData)
    {
        if (settings.currentMode != Mode.NAVIGATION) return;
        reaching = true;
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (settings.currentMode != Mode.NAVIGATION) return;
        reaching = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (reaching)
        {
            Camera.main.transform.parent.position += Camera.main.transform.forward * 0.05f;
        }
    }
}