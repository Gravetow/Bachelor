using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HoverHighlighter : IInitializable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private MaterialSettings materialSettings;

    private MeshRenderer previousHoverTarget;
    private Material previousHoverTargetMaterial;

    public void Initialize()
    {
        _signalBus.Subscribe<SelectSignal>(Highlight);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectSignal>(Highlight);
    }

    private void Highlight(SelectSignal select)
    {
        MeshRenderer meshRenderer = select.selectedGameObject.GetComponent<MeshRenderer>();

        if (previousHoverTarget != null)
        {
            previousHoverTarget.material = previousHoverTargetMaterial;
        }

        if (meshRenderer != null)
        {
            previousHoverTargetMaterial = meshRenderer.material;
            previousHoverTarget = meshRenderer;

            meshRenderer.material = materialSettings.hoverMaterial;
        }
    }
}