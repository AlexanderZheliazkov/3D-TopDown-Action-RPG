using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentDisplay : MonoBehaviour
{
    [SerializeField] private Transform rightHandPlaceholder;
    [SerializeField] private Transform leftHandPlaceholder;

    [SerializeField] private GameObject equipmentMeshRenderersParent;
    [SerializeField] private SkinnedMeshRenderer targetMesh;

    private List<SkinnedMeshRenderer> equipmentMeshRenderers = new List<SkinnedMeshRenderer>();

    private GameObject rightHandItem;
    private GameObject leftHandItem;

    public void ChangeWeapon(Weapon _newWeapon)
    {
        if (_newWeapon == null)
            return;

        if (leftHandItem != null)
        {
            Destroy(leftHandItem);
        }

        if (rightHandItem != null)
        {
            Destroy(rightHandItem);
        }

        if (_newWeapon.RightHandPrefab != null)
        {
            rightHandItem = Instantiate(_newWeapon.RightHandPrefab, rightHandPlaceholder);
        }

        if (_newWeapon.LeftHandPrefab != null)
        {
            leftHandItem = Instantiate(_newWeapon.LeftHandPrefab, leftHandPlaceholder);
        }
    }

    public void OnArmorChanged(Armor _newEquipment)
    {
        if (_newEquipment == null) return;

        foreach (var meshRenderer in equipmentMeshRenderers)
        {
            Destroy(meshRenderer.gameObject);
        }

        equipmentMeshRenderers.Clear();

        foreach (var meshRenderer in _newEquipment.skinnedMeshRenderers)
        {
            var newMesh = Instantiate(meshRenderer, equipmentMeshRenderersParent.transform);
            newMesh.rootBone = targetMesh.rootBone;
            newMesh.bones = targetMesh.bones;
            equipmentMeshRenderers.Add(newMesh);
        }
    }
}