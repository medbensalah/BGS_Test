using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        PlayerData.Instance.OnMoneyChanged += UpdateMoney;
        UpdateMoney();
    }

    private void OnDestroy()
    {
        PlayerData.Instance.OnMoneyChanged -= UpdateMoney;
    }

    private void UpdateMoney()
    {
        _moneyText.text = $"{PlayerData.Instance.Money} $";
    }
}
