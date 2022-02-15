using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    int currentDiamond;
    int currentGold;
    [SerializeField] private SaveSystem _saveSystem;

    public int CurrentDiamond
    {
        get => currentDiamond;
        private set
        {
            currentDiamond = value;
            _saveSystem.DiamondAmount = currentDiamond;
            UpdateUI();
        }
    }
    public int CurrentGold
    {
        get => currentGold;
        private set
        {
            currentGold = value;
            _saveSystem.GoldAmount = currentGold;
            UpdateUI();
        }
    }

    private void Start()
    {
        BuildingEvents.BuildingConstructed +=
            (buildingSO) => Buy(buildingSO.GoldAmount, buildingSO.DiamondAmount); 
        ShopEvents.IncreaseCurrency += Earn; 
        
        if (_saveSystem.DiamondAmount == -1)
        {
            CurrentDiamond = 10;
            CurrentGold = 10;
        }
        else
        {
            CurrentDiamond = _saveSystem.DiamondAmount;
            CurrentGold = _saveSystem.GoldAmount;
        }
    }

    public void IncreaseGold(int amount)
    {
        CurrentGold += amount;
    }
    public void IncreaseDiamond(int amount)
    {
        CurrentDiamond += amount;
    }
    public void DecreaseGold(int amount)
    {
        CurrentGold -= amount;
    }
    public void DecreaseDiamond(int amount)
    {
        CurrentDiamond -= amount;
    }

    public bool CanBuy(int goldAmount, int diamondAmount) => IsEnoughDiamond(diamondAmount) && IsEnoughGold(goldAmount);
    public bool IsEnoughGold(int amount) => CurrentGold >= amount;
    public bool IsEnoughDiamond(int amount) => CurrentDiamond >= amount;

    public void Buy(int goldAmount, int diamondAmount)
    {
        if (!CanBuy(goldAmount, diamondAmount))
            Debug.LogError("Not enough money");
        DecreaseDiamond(diamondAmount);
        DecreaseGold(goldAmount);
    } 
    public void Earn(int goldAmount, int diamondAmount)
    {
        IncreaseDiamond(diamondAmount);
        IncreaseGold(goldAmount);
    }

    private void UpdateUI()
    {
        ShopEvents.UpdateCurrency?.Invoke(currentGold, currentDiamond);
    }
}
