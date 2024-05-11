using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public enum EquipSlot
    {
        Head,
        Chest,
        ShouldersLeft,
        ShouldersRight,
        ArmsLeft,
        ArmsRight,
        GlovesLeft,
        GlovesRight,
        Legs,
        ShoesLeft,
        ShoesRight
    }
    
    public static PlayerData Instance;

    [field: SerializeField] public ulong Money { get; private set; }
    public Dictionary<Item, uint> Inventory { get; private set; } = new Dictionary<Item, uint>();
    
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
}
