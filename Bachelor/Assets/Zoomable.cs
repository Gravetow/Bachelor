using UnityEngine;
using DG.Tweening;
using HoloToolkit.Unity.InputModule;
using Zenject;

public class Zoomable : MonoBehaviour, IInputHandler
{
    public InteractiveMeshCursor cursor;
    private Vector3 startPosition;

    [Inject]
    public ModeSettings settings;

    public void OnInputDown(InputEventData eventData)
    {
        if (settings.currentMode != Mode.VIEW) return;

        startPosition = Camera.main.transform.position;
        Camera.main.transform.parent.DOMove((cursor.Position - startPosition) * 0.25f, 0.5f);
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (settings.currentMode != Mode.VIEW) return;

        Camera.main.transform.parent.DOMove(startPosition, 0.5f);
    }
}