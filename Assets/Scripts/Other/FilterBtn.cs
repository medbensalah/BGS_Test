using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterBtn : MonoBehaviour
{
    public static Action<Button> OnFilterBtnClicked;
    private Animator _animator;
    private static readonly int Active = Animator.StringToHash("active");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnFilterBtnClicked += FilterBtnClicked;
    }

    private void OnDestroy()
    {
        OnFilterBtnClicked -= FilterBtnClicked;
    }

    private void FilterBtnClicked(Button btn)
    {
        if (btn == GetComponent<Button>())
        {
            _animator.SetBool(Active, true);
        }
        else
        {
            _animator.SetBool(Active, false);
        }
    }
}