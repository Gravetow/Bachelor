using UnityEngine;
using Zenject;

public class GazeInput : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            _signalBus.Fire(new SelectSignal() { selectedGameObject = hit.collider.gameObject, position = hit.point });
        }
        else
        {
            _signalBus.Fire(new DeselectSignal());
        }
    }
}