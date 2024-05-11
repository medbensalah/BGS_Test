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

        if (_buyButton.interactable)
        {
        }

        if (_sellButton.interactable)
        {
            _sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Sell: {selectedItem.ItemPrice / 2}";
        }
    }

    private bool CanBuy(Item selectedItem)
    {
        bool canBuy = _item.ItemPrice <= PlayerData.Instance.Money;
        if (!canBuy) 
            _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough money";
        else
            _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Buy: {selectedItem.ItemPrice}";
        return canBuy;
    }

    public void Buy()
    {
        if (_onDelay) return;
        if (CanBuy(_item))
        {
            Audio.Instance.PlayOneShot(Audio.Instance._success);
            PlayerData.Instance.RemoveMoney(_item.ItemPrice);
            PlayerData.Instance.AddItem(_item);
            _buyButton.interactable = CanBuy(_item);
            _sellButton.interactable = CanSell(_item);
            StartCoroutine(SetDelay());
        }
        else
        {
            Audio.Instance.PlayOneShot(Audio.Instance._fail);
        }
    }

    private bool CanSell(Item selectedItem)
    {
        bool canSell;
        uint itemCount = PlayerData.Instance.Inventory.TryGetValue(selectedItem, out var value) ? value : 0;
        if (itemCount == 1)
        {
            canSell = !PlayerData.Instance.IsEquipped(selectedItem);
            if (!canSell)
            {
                _sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Cannot sell equipped item";
                return false;
            }
        }

        canSell = itemCount > 0;
        if (!canSell)
            _sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"No item to sell";
        else
            _sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Sell: {selectedItem.ItemPrice / 2}";
        return canSell;
    }

    public void Sell()
    {
        if (_onDelay) return;
        if (CanSell(_item))
        {
            Audio.Instance.PlayOneShot(Audio.Instance._success);
            PlayerData.Instance.AddMoney(_item.ItemPrice / 2);
            PlayerData.Instance.RemoveItem(_item);
            _buyButton.interactable = CanBuy(_item);
            _sellButton.interactable = CanSell(_item);
            StartCoroutine(SetDelay());
        }
        else
        {
            Audio.Instance.PlayOneShot(Audio.Instance._fail);
        }
    }

    private IEnumerator SetDelay()
    {
        _onDelay = true;
        yield return new WaitForSeconds(_delay);
        _onDelay = false;
    }
}