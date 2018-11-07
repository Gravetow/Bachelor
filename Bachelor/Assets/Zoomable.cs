using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HoloToolkit.Unity.InputModule;

public class Zoomable : MonoBehaviour, IInputHandler
{
    public InteractiveMeshCursor cursor;
    private Vector3 startPosition;

    public void OnInputDown(InputEventData eventData)
    {
        startPosition = Camera.main.transform.position;
        Camera.main.transform.parent.DOMove((cursor.Position - startPosition) * 0.25f, 0.5f);
    }

    public void OnInputUp(InputEventData eventData)
    {
        Camera.main.transform.parent.DOMove(startPosition, 0.5f);
    }
}