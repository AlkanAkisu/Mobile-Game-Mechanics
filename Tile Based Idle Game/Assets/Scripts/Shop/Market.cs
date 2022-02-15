using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;
    void Start()

    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanBuy(int goldAmount, int diamondAmount)
    {
        return currencyManager.CanBuy(goldAmount, diamondAmount);
    }
}
