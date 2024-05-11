using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact()
    {
        //Disable Current control scheme
        Debug.Log("Interacting with shop");
    }
    
    public override void OnPlayerEnter(GameObject player)
    {
        Debug.Log("Player entered shop trigger");
        player.GetComponent<PlayerMisc>().ShowInteractionBubble();
    }
    
    public override void OnPlayerExit(GameObject player)
    {
        Debug.Log("Player exited shop trigger");
        player.GetComponent<PlayerMisc>().HideInteractionBubble();
    }
}
