using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Vector2Variable moveInput;

    [Header("MouseSettings")]
    [SerializeField]
    private Vector2Variable mouseInput;
    [SerializeField]
    private float mouseSensitivity = 1;
    [SerializeField]
    private bool invertY = false;
    [SerializeField]
    private bool showMouse = false;

    [Header("Combat")]
    [SerializeField]
    private GameEvent onAttackTrigger;
    [SerializeField]
    private KeyCode attackKey;

    private void UpdateMoveInput()
    {
        moveInput.Value.x = Input.GetAxis("Vertical");
        moveInput.Value.y = Input.GetAxis("Horizontal");
    }

    private void UpdateMouseInput()
    {
        mouseInput.Value.x = (Input.GetAxis("Mouse X") * mouseSensitivity);
        mouseInput.Value.y = invertY ? (Input.GetAxis("Mouse Y") * mouseSensitivity) : -(Input.GetAxis("Mouse Y") * mouseSensitivity);
    }

    private void UpdateAttackTrigger()
    {
        if (Input.GetKey(attackKey))
        {
            onAttackTrigger.Raise();
        }
    }

    private void Start()
    {
        if (!showMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        UpdateMoveInput();
        UpdateMouseInput();
        UpdateAttackTrigger();
    }
}
