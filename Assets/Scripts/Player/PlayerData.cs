using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{    
    public static PlayerData Instance;

    [field: SerializeField] public ulong Money { get; private set; }
    public Dictionary<Item, uint> Inventory { get; private set; } = new Dictionary<Item, uint>();
    public Dictionary<ItemSlot, Item?> Equipments { get; private set; } = new Dictionary<ItemSlot, Item?>();

    [SerializeField] private SpriteRenderer headAnchor;
    [SerializeField] private SpriteRenderer chestAnchor;
    [SerializeField] private SpriteRenderer shouldersLeftAnchor;
    [SerializeField] private SpriteRenderer shouldersRightAnchor;
    [SerializeField] private SpriteRenderer armsLeftAnchor;
    [SerializeField] private SpriteRenderer armsRightAnchor;
    [SerializeField] private SpriteRenderer glovesLeftAnchor;
    [SerializeField] private SpriteRenderer glovesRightAnchor;
    [SerializeField] private SpriteRenderer legsAnchor;
    [SerializeField] private SpriteRenderer shoesLeftAnchor;
    [SerializeField] private SpriteRenderer shoesRightAnchor;
    
    public Action OnMoneyChanged;
    public Action OnInventoryChanged;
    [Header("FOR MOCK DATA ONLY")]
    [SerializeField] private ShopItems_so ShopItems_so;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        // MOCK DATA
        //add equipment slots
        foreach (ItemSlot slot in Enum.GetValues(typeof(ItemSlot)))
        {
            Equipments.Add(slot, null);
        }
        // add to inventory
        foreach (Item item in ShopItems_so.shopItems.Take(11))
        {
            AddItem(item);
            EquipItem(item);
        }
    }
    
    public bool AddMoney(ulong amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke();
        return true;
    }
    
    public bool RemoveMoney(ulong amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            OnMoneyChanged?.Invoke();
            return true;
        }
        return false;
    }
    
    public bool AddItem(Item item)
    {
        if (Inventory.ContainsKey(item))
        {
            Inventory[item]++;
        }
        else
        {
            Inventory.Add(item, 1);
        }
        OnInventoryChanged?.Invoke();
        return true;
    }
    
    public bool RemoveItem(Item item)
    {
        if (Inventory.ContainsKey(item))
        {
            if (Inventory[item] > 1)
            {
                Inventory[item]--;
            }
            else
            {
                Inventory.Remove(item);
            }
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }
 
    public void EquipItem(Item item)
    {
        Equipments[item.ItemSlot] = item;
        switch (item.ItemSlot)
        {
            case ItemSlot.Head:
                headAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.Chest:
                chestAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ShouldersLeft:
                shouldersLeftAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ShouldersRight:
                shouldersRightAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ArmsLeft:
                armsLeftAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ArmsRight:
                armsRightAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.GlovesLeft:
                glovesLeftAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.GlovesRight:
                glovesRightAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.Legs:
                legsAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ShoesLeft:
                shoesLeftAnchor.sprite = item.ItemIcon;
                break;
            case ItemSlot.ShoesRight:
                shoesRightAnchor.sprite = item.ItemIcon;
                break;
            default:
                Debug.LogError("Invalid ItemSlot");
                break;
        }
    }
    
    public bool IsEquipped(Item item)
    {
        return Equipments.ContainsValue(item);
    }
}
