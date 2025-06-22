using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCam : MonoBehaviour
{

    private Camera arCamera;
    void Awake()
    {
        arCamera = Camera.main; // AR Camera
    }

    void Update()
    {
        Vector3 direction = arCamera.transform.position - transform.position;
        direction.y = 0; // Ignore the Y-axis

        // Set the GameObject to look at the camera's direction on the Y-axis only.
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
