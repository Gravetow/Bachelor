using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public Camera Headcamera;

    private void Start()
    {
        transform.position = Headcamera.transform.position;
    }
}