using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text diamondText;
    [SerializeField] TMPro.TMP_Text goldText;
    void Awake()
    {
        ShopEvents.UpdateCurrency += UpdateCurrencyUI;
    }

    private void UpdateCurrencyUI(int gold, int diamond)
    {
        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();
    }
}
