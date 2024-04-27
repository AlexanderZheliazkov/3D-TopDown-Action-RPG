using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public partial class EquipmentSlotUI : MonoBehaviour
{
    public Equipment CurrentEquipment { get; private set; }

    public Toggle SelectToggle;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemImage;

    [SerializeField] private TextMeshProUGUI damageBonus;
    [SerializeField] private TextMeshProUGUI healthBonus;
    [SerializeField] private TextMeshProUGUI defenceBonus;

    public void UpdateItemInfo(Equipment _equipment)
    {
        this.CurrentEquipment = _equipment;

        this.itemName.text = _equipment.name;
        this.itemImage.sprite = _equipment.Icon;

        this.damageBonus.text = _equipment.DamageModifier.ToString();
        this.healthBonus.text = _equipment.HealthModifier.ToString();
        this.defenceBonus.text = _equipment.DefenceModifier.ToString();
    }
}