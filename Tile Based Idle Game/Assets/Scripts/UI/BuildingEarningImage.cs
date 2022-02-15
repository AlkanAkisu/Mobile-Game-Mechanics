using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening;

public class BuildingEarningImage : MonoBehaviour
{
   [SerializeField] private float duration;
   [SerializeField] private float yMove;
   
   public float Duration => AddNoise(duration, 0.5f);
   public float YMove => AddNoise(yMove, 50f);
   public float StartX => AddNoise(transform.position.x, 120f);
    
   public void StartTheAnimation()
   {
      transform.position = transform.position.ChangeVector(x: StartX);
      transform.GetComponent<Image>().CrossFadeAlpha(0.5f,duration,false);
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

   
   
   
}
