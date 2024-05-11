using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField] private GameObject itemEntryPrefab;
    
    public void Init(List<Item> items, bool isShop)
    {   
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in items)
        {
            var itemEntry = Instantiate(itemEntryPrefab, transform);
            itemEntry.GetComponent<ItemEntry>().Init(item, isShop);
        }
    }
}
