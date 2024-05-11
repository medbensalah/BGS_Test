using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    private Item _item;
    
    public void Init(Item item)
    {
        this._item = item;
        _itemIcon.sprite = item.ItemIcon;
        _itemName.text = item.ItemName;

        _itemPrice.text = item.ItemPrice.ToString();
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ShopScreen.OnSelectItem?.Invoke(_item);
        });
    }
}
