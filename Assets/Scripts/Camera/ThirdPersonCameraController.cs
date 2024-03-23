using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField]
    private QuaternionVariable cameraRotation;

    [SerializeField]
    private Vector2Variable mouseInput;

    [SerializeField]
    private Vector3Variable playerPosition;
    [SerializeField]
    private Vector3 offset;

    private float rotationX;
    private float rotationY;

    void Start()
    {

    }

    void Update()
    {
        transform.position = playerPosition.Value + offset;
        rotationY += mouseInput.Value.x;
        rotationX += mouseInput.Value.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationX, rotationY, 0), .5f);
        cameraRotation.Value = transform.rotation;
    }

    private void FixedUpdate()
    {

    }
}
