using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ManipulationView : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    [SerializeField]
    private ListView list;

    private void Awake()
    {
        _signalBus.Subscribe<OpenManipulationToolSignal>(ActivateManipulationTool);
        _signalBus.Subscribe<CloseManipulationToolSignal>(DeactivateManipulationTool);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<OpenNavigationToolSignal>(ActivateManipulationTool);
        _signalBus.Unsubscribe<CloseNavigationToolSignal>(DeactivateManipulationTool);
    }

    private void ActivateManipulationTool()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        transform.LookAt(Camera.main.transform);

        gameObject.SetActive(true);

        list.Show();
    }

    private void DeactivateManipulationTool()
    {
        gameObject.SetActive(false);
        list.Hide();
    }
}