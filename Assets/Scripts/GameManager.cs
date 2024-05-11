using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private PlayerInput _playerInput;
    
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
        Application.targetFrameRate = 60;
    }
    
    public void DisbleInput()
    {
        _playerInput.enabled = false;
    }
    
    public void EnableInput()
    {
        _playerInput.enabled = true;
    }
}
