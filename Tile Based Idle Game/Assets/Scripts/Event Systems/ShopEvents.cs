using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvents : MonoBehaviour
{
    private static ShopEvents _instance;

    public static ShopEvents Instance { get { return _instance; } }

    [SerializeField] Building debugVal;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static Action<int, int> IncreaseCurrency = delegate { };
    public static Action<int, int> UpdateCurrency = delegate { };







}
