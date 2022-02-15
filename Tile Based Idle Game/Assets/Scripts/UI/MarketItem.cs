using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MarketItem : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] BuildingSO buildingSO;
    [SerializeField] TMP_Text diamondText;
    [SerializeField] RectTransform diamondBox;
    [SerializeField] TMP_Text goldText;
    [SerializeField] RectTransform goldBox;
    [SerializeField] Image image;

    #endregion

    #region Private Fields

    #endregion

    #region Public Properties
    public int GoldAmount => buildingSO.GoldAmount;
    public int DiamondAmount => buildingSO.DiamondAmount;
    public bool CanBuy =>  market.CanBuy(GoldAmount, DiamondAmount);
    #endregion

    #region Dependencies
    private Market market;
    private BuildingCreator creator;
    private Image wholeImage;

    #endregion


    private void Awake()
    {
        market = FindObjectOfType<Market>();
        creator = FindObjectOfType<BuildingCreator>();
        Invoke(nameof(CheckIfBuyable),0.5f);
        wholeImage = GetComponent<Image>();
    }

    private void CheckIfBuyable()
    {
        if (!CanBuy)
            wholeImage.CrossFadeAlpha(0.5f,0,false);
        else
            wholeImage.CrossFadeAlpha(1f,0,false);
        
        Invoke(nameof(CheckIfBuyable),0.5f);

    }
    

    private void OnValidate() => UpdateUI();

    private void UpdateUI()
    {
        goldText.text = GoldAmount.ToString();
        if (GoldAmount == 0)
            goldBox.gameObject.SetActive(false);
        else
            goldBox.gameObject.SetActive(true);
        if (DiamondAmount == 0)
            diamondBox.gameObject.SetActive(false);
        else
            diamondBox.gameObject.SetActive(true);

        diamondText.text = DiamondAmount.ToString();
        image.sprite = buildingSO.Sprite;
    }


    public void TryToBuy()
    {
        
        if (!CanBuy)
        {
            Debug.Log($"Can not buy");
            return;
        }

        creator.CreateBuilding(buildingSO);

    }
}
