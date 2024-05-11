using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDetails : MonoBehaviour
{
    private Item _item;
    
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    
    [SerializeField] private GameObject _success;
    [SerializeField] private GameObject _fail;

    [SerializeField] private float _delay = 0.1f;
    private bool _onDelay = false;    
    
    public void Init(Item selectedItem)
    {
        _item = selectedItem;
        
        _itemIcon.sprite = selectedItem.ItemIcon;
        _itemName.text = selectedItem.ItemName;
        
        _buyButton.interactable = CanBuy(selectedItem);
        _sellButton.interactable = CanSell(selectedItem);
        
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy: {selectedItem.ItemPrice}";
        _sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Sell: {selectedItem.ItemPrice / 2}";
    }
    
    private bool CanBuy(Item selectedItem)
    {
        return _item.ItemPrice <= PlayerData.Instance.Money;
    }
    
    public void Buy()
    {
        if (_onDelay) return;
        if (CanBuy(_item))
        {
            //TODO: play music cue
            PlayerData.Instance.RemoveMoney(_item.ItemPrice);
            PlayerData.Instance.AddItem(_item);
            _buyButton.interactable = CanBuy(_item);
            _sellButton.interactable = CanSell(_item);
            StartCoroutine(SetDelay());
        }
    }
    
    private bool CanSell(Item selectedItem)
    {
        uint itemCount = PlayerData.Instance.Inventory.TryGetValue(selectedItem, out var value) ? value : 0;
        return itemCount > 0;
    }
    
    public void Sell()
    {
        if (_onDelay) return;
        if (CanSell(_item))
        {
            //TODO: play music cue
            PlayerData.Instance.AddMoney(_item.ItemPrice / 2);
            PlayerData.Instance.RemoveItem(_item);
            _buyButton.interactable = CanBuy(_item);
            _sellButton.interactable = CanSell(_item);
            StartCoroutine(SetDelay());
        }
    }
    
    private IEnumerator SetDelay()
    {
        _onDelay = true;
        yield return new WaitForSeconds(_delay);
        _onDelay = false;
    }
}
