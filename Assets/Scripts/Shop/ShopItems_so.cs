using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to menu
[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObject/ShopItem")]
public class ShopItems_so : ScriptableObject
{
    public List<Item> shopItems = new List<Item>();
}
