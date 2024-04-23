using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public QuaternionVariable CameraRotation;
    public Vector3Variable PlayerPosition;
    public Vector3Variable CameraPosition;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Camera cam;

    void Start()
    {
    }

    private void Update()
    {
        if (transform.position != PlayerPosition.Value)
        {
            transform.position = PlayerPosition.Value + offset;
        }

        cam.transform.LookAt(PlayerPosition.Value);

        if (CameraRotation.Value != transform.rotation)
        {
            CameraRotation.Value = cam.transform.rotation;
        }

        if (CameraPosition.Value != cam.transform.position)
        {
            CameraPosition.Value = cam.transform.position;
        }
    }
}