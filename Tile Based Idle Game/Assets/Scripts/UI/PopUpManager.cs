using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
   private Camera cam;
  [SerializeField] private GameObject goldImage;
  [SerializeField] private GameObject diamondImage;

   void Awake()
   {
      cam = Camera.main;
   }
   public void CreatePopUp(Vector3 pos, (int,int) amounts)
   {
      var newVect = cam.WorldToScreenPoint(pos);

      for (int i = 0; i < amounts.Item1; i++)
      {
         var gold = Instantiate(goldImage, newVect, Quaternion.identity,transform);
         gold.GetComponent<BuildingEarningImage>().StartTheAnimation();
         
      }

      for (int i = 0; i < amounts.Item2; i++)
      {
         var diamond = Instantiate(diamondImage, newVect, Quaternion.identity,transform);
         diamond.GetComponent<BuildingEarningImage>().StartTheAnimation();
      }
      
      
   }
}
