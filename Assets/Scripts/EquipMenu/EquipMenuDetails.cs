using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenuDetails : MonoBehaviour
{
    private Item _item;
    
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _equipButton;

    public void Init(Item selectedItem)
    {
        _item = selectedItem;
        
        _itemIcon.sprite = selectedItem.ItemIcon;
        _itemName.text = selectedItem.ItemName;
        _equipButton.interactable = CanEquip(selectedItem);
    }
    
    private bool CanEquip(Item selectedItem)
    {
        bool canEquip = !PlayerData.Instance.IsEquipped(selectedItem);
        _equipButton.GetComponentInChildren<TextMeshProUGUI>().text = canEquip ? "Equip" : "Equipped";
        return canEquip;
    }
    
    public void EquipItem()
    {
        PlayerData.Instance.EquipItem(_item);
        _equipButton.interactable = CanEquip(_item);
    }
}
