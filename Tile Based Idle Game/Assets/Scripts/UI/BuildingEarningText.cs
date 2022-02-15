using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using DG.Tweening.Core;

public class BuildingEarningText : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float yMove;
    [SerializeField] private TMPro.TMP_Text floatingText;
    
    public float Duration => AddNoise(duration, 0.5f);
    public float YMove => AddNoise(yMove, 50f);
    public float StartX => AddNoise(transform.position.x, 120f);
    
   public void StartTheAnimation()
    {
        transform.position = transform.position.ChangeVector(x: StartX);
        transform.DOMoveY(transform.position.y + YMove, duration).onComplete += DestroyItself;
        

    }
   

    void DestroyItself()
    {
        Destroy(gameObject);
    }

    public float AddNoise(float val, float noise)
    {
        var rand = new System.Random();
        var minus = rand.Next(2) == 0 ? 1 : -1;
        noise = noise * minus * (float)rand.NextDouble();
        return val + noise;


    }

    public BuildingEarningText SetText(int earning)
    {
        floatingText.text = earning.ToString();
        return this;
    }

    public virtual BuildingEarningText SetDiamond()
    {
        floatingText.color = Color.blue;
        return this;

    }
    
    public virtual BuildingEarningText SetGold()
    {
        floatingText.color = Color.yellow;
        return this;

    }
  
}
