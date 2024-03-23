using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider vfxSlider;
    [SerializeField]
    private QuaternionVariable cameraRotation;

    [SerializeField]
    private float smoothVfxSlider = 0.5f;

    public void UpdateHealth(float _healthPercent)
    {
        healthSlider.value = _healthPercent;
    }

    private void UpdateVfxSlider()
    {
        if (vfxSlider != null && vfxSlider.value != healthSlider.value)
        {
            vfxSlider.value = Mathf.Lerp(vfxSlider.value, healthSlider.value, smoothVfxSlider);
        }
    }

    private void FixedUpdate()
    {
        UpdateVfxSlider();
        parentObject.transform.rotation = cameraRotation.Value;
    }
}
