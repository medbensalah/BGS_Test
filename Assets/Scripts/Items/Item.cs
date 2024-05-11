using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ItemType
{
    All = -1,
    Head,
    Chest,
    Shoulders,
    Arms,
    Gloves,
    Legs,
    Shoes
}
[Serializable]
public enum ItemSlot
{
    All = -1,
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

[Serializable]
public struct Item
{
    public string ItemName;
    public Sprite ItemIcon;
    public uint ItemPrice;
    public ItemType ItemType;
    public ItemSlot ItemSlot;
    
    //define == operator
    public static bool operator ==(Item a, Item b)
    {
        return a.ItemName == b.ItemName;
    }
    
    //define != operator
    public static bool operator !=(Item a, Item b)
    {
        return a.ItemName != b.ItemName;
    }
}
