using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMisc : MonoBehaviour
{
    public static PlayerMisc Instance { get; private set; }
     
    [SerializeField] private EquipMenuScreen equipMenuScreen;
    public Interactable Interactable { get; set; }
    
    [SerializeField]
    private CanvasGroup _interactionBubble;
    
    private IEnumerator _cr_ShowInteractionBubble;
    private IEnumerator _cr_HideInteractionBubble;
    
    private float animationDuration = 0.2f;
    private GameObject UICanvas;

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

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        UICanvas = GameObject.Find("Global").transform.Find("UI").gameObject;
        _cr_ShowInteractionBubble = cr_ShowInteractionBubble();
        _cr_HideInteractionBubble = cr_HideInteractionBubble();
    }

    public void OnInteract()
    {
        if (Interactable == null) return;
        Interactable.Interact();
    }
    
    public void OnEquipMenu()
    {
        //open equip menu
        EquipMenuScreen ems = Instantiate(equipMenuScreen, UICanvas.transform);
        ems.Init();
    }
    
    public void ShowInteractionBubble()
    {
        if (Interactable)
        {
            StopCoroutine(_cr_ShowInteractionBubble);
            _cr_ShowInteractionBubble = cr_ShowInteractionBubble();
            StartCoroutine(_cr_ShowInteractionBubble);
        }
    }
    
    public void HideInteractionBubble()
    {
        StopCoroutine(_cr_HideInteractionBubble);
        _cr_HideInteractionBubble = cr_HideInteractionBubble();
        StartCoroutine(_cr_HideInteractionBubble);
    }
    
    private IEnumerator cr_ShowInteractionBubble()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < animationDuration)
        {
            _interactionBubble.alpha = Mathf.Lerp(0, 1, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (elapsedTime >= animationDuration)
        {
            _interactionBubble.alpha = 1;
        }
    }
    
    private IEnumerator cr_HideInteractionBubble()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < animationDuration)
        {
            _interactionBubble.alpha = Mathf.Lerp(1, 0, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (elapsedTime >= animationDuration)
        {
            _interactionBubble.alpha = 0;
        }
    }
}
