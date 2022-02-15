using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine.PlayerLoop;

public class Building : MonoBehaviour
{

    #region Serialize Fields
    [SerializeField] BuildingSO buildingStat;
    [SerializeField] private float durationOfAnim;
    [SerializeField] private GameObject floatingText;

    #endregion

    #region Private Fields
    
    private bool isPlaced;
    private TweenerCore<Vector3, Vector3, VectorOptions> lastMove;
    private int rotateTime;
    private SpriteRenderer spriteRenderer;
    private Camera cam;
    private Transform canvas;
    private PopUpManager popUpManager;

    #endregion

    #region Public Properties

    public int RotateTime => rotateTime;

    public BuildingSO BuildingStat
    {
        get => buildingStat;
        set
        {
            buildingStat = value;

        }
    }
    public bool[,] buildingMatrix => buildingStat.BuildingMatrix;

    public bool IsPlaced
    {
        get => isPlaced; set
        {
            if (value)
                Placed();
            isPlaced = value;
        }
    }

    public int GoldIncome => buildingStat.GoldEarningPerSecond;
    public int DiamondIncome => buildingStat.DiamondEarningPerSecond;

    public Vector3 FloatingTextSpawnPos => transform.position + Vector3.up * 0.75f;

    #endregion

    void Awake()
    {
        cam = Camera.main;
        canvas = FindObjectOfType<Canvas>().transform;
        popUpManager = FindObjectOfType <PopUpManager>();
    }

    public void Init()
    {
        IsPlaced = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = buildingStat.Sprite;
        BuildableColor();
    }

   
    public void Earn()
    {
        ShopEvents.IncreaseCurrency?.Invoke(GoldIncome, DiamondIncome);
        Invoke(nameof(Earn), 1f);
        popUpManager.CreatePopUp(FloatingTextSpawnPos,(GoldIncome,DiamondIncome));
    }
    
    

    public void BuildableColor()
    {
        var color = spriteRenderer.color;
        color = Color.green;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    public void NonBuildableColor()
    {
        var color = spriteRenderer.color;
        color = Color.red;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }
    private void OnValidate()
    {
        BuildingStat = buildingStat;
    }

    private void Placed()
    {
        Debug.Log($"{buildingStat.name} placed");
        var color = spriteRenderer.color;
        color = Color.white;
        color.a = 1f;
        spriteRenderer.color = color;
        var z = transform.position.z + 1;
        transform.position = transform.position.ChangeVector(z: z);
        Invoke(nameof(Earn), 1f);

        //Instantiate(floatingText, FloatingTextSpawnPos, Quaternion.identity);
    }


    public void Rotate(int rotateTime, bool animate = true)
    {
        this.rotateTime = rotateTime;
        rotateTime = rotateTime % 4;
        var endVal = new Vector3(0f, 0f, rotateTime * -90f);
        if(animate)
            transform.DORotate(endVal, durationOfAnim).SetEase(Ease.InOutQuad);
        else
            transform.DORotate(endVal, 0).SetEase(Ease.InOutQuad);
    }


    public void GoPosition(Vector3 position)
    {
        lastMove?.Kill();
        lastMove = transform.DOMove(position, durationOfAnim).SetEase(Ease.InOutQuad);
    }

}
