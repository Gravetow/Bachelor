using UnityEngine;
using Zenject;

public class GazeInput : MonoBehaviour
{
    [Inject]
    public readonly SignalBus signalBus;

    public void Update()
    {
        if (transform.eulerAngles.x > 15)
        {
            signalBus.Fire<LookDownSignal>();
            Debug.Log("oho");
        }

        Debug.DrawRay(Camera.main.transform.position,
                   Camera.main.transform.forward * 20.0f,
                   Color.green);
    }

    //RaycastHit hitInfo;
    //if (Physics.Raycast(
    //        Camera.main.transform.position,
    //        Camera.main.transform.forward,
    //        out hitInfo,
    //        20.0f,
    //        Physics.DefaultRaycastLayers))
    //{
    //    Debug.Log(hitInfo.collider.gameObject, gameObject);
    //    // If the Raycast has succeeded and hit a hologram
    //    // hitInfo's point represents the position being gazed at
    //    // hitInfo's collider GameObject represents the hologram being gazed at
    //}
}