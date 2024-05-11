using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : Interactable
{
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private ShopItems_so shopItems;
    private GameObject UICanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("Global").transform.Find("UI").gameObject;
    }

    public override void Interact()
    {
        ShopScreen ss = Instantiate(shopScreen, UICanvas.transform);
        ss.Init(shopItems);
    }
    
    public override void OnPlayerEnter(GameObject player)
    {
        player.GetComponent<PlayerMisc>().ShowInteractionBubble();
    }
    
    public override void OnPlayerExit(GameObject player)
    {
        player.GetComponent<PlayerMisc>().HideInteractionBubble();
    }
}
