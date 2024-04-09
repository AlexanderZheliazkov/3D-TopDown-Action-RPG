using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEquipment : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private Transform rightHandPlaceholder;
    [SerializeField]
    private Transform leftHandPlaceholder;

    [Header("Equipment")]
    [SerializeField]
    private Equipment equipment;
    [SerializeField]
    private GameObject equipmentMeshRenderersParent;
    [SerializeField]
    private SkinnedMeshRenderer targetMesh;

    private List<SkinnedMeshRenderer> equipmentMeshRenderers = new List<SkinnedMeshRenderer>();

    private GameObject rightHandItem;
    private GameObject leftHandItem;

    public void ChangeWeapon(Weapon _newWeapon)
    {
        if (_newWeapon == null)
            return;

        if (_newWeapon.RightHandPrefab != null)
        {
            if (rightHandItem != null)
            {
                Destroy(rightHandItem);
            }
            rightHandItem = Instantiate(_newWeapon.RightHandPrefab, rightHandPlaceholder);
        }

        if (_newWeapon.LeftHandPrefab != null)
        {
            if (leftHandItem != null)
            {
                Destroy(leftHandItem);
            }
            leftHandItem = Instantiate(_newWeapon.LeftHandPrefab, leftHandPlaceholder);
        }
    }

    public void OnEquipmentChanged(Equipment _newEquipment)
    {
        if (equipment == null) return;

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
