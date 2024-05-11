using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMisc))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField]
    private float _speed = 5.0f;

    
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private static readonly int anim_f_moveSpeed = Animator.StringToHash("f_moveSpeed");


    private Transform _visual;

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

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _visual = transform.Find("Visual");
    }
    public void OnWalk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _rb.velocity = input * _speed;
            _animator.SetFloat(anim_f_moveSpeed, _speed);
            Vector3 scale = _visual.localScale;
            scale.x = Mathf.Sign(input.x) * Mathf.Abs(scale.x);
            _visual.localScale = scale;
        }

        if (context.canceled)
        {
            _rb.velocity = Vector2.zero;
            _animator.SetFloat(anim_f_moveSpeed, 0.0f);
        }
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<PlayerMisc>().OnInteract();
        }
    }
    
    public void OnEquipMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Equip Menu");
        }
    }
}
