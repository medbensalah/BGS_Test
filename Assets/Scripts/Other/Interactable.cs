using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMisc>().Interactable = this;
            OnPlayerEnter(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMisc p = other.GetComponent<PlayerMisc>();
            if (p.Interactable == this)
            {
                p.Interactable = null;
                OnPlayerExit(other.gameObject);
            }
        }
    }

    public virtual void Interact() { }
    
    public virtual void OnPlayerEnter(GameObject player) { }
    
    public virtual void OnPlayerExit(GameObject player) { }
}
