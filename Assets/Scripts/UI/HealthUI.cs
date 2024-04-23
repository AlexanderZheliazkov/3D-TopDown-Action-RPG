using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider vfxSlider;
    [SerializeField] private QuaternionVariable cameraRotation;
    [SerializeField] private Vector3Variable cameraPosition;
    [SerializeField] private float smoothVfxSlider = 0.5f;

    public void UpdateHealth(float _healthPercent)
    {
        healthSlider.value = _healthPercent;
    }

    private void UpdateVfxSlider()
    {
        if (vfxSlider.value - healthSlider.value > 0.001f)
        {
            vfxSlider.value = Mathf.Lerp(vfxSlider.value, healthSlider.value, smoothVfxSlider);
        }
    }

    private void FixedUpdate()
    {
        UpdateVfxSlider();
        parentObject.transform.LookAt(cameraPosition.Value); //Todo: Find better way to rotate health ui
        // parentObject.transform.rotation = cameraRotation.Value;
    }
}