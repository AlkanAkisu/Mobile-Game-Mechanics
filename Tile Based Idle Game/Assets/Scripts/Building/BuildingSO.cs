using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingSO : ScriptableObject
{
    [SerializeField] BuildingLayout buildingLayout;
    [SerializeField] Sprite sprite;
    [SerializeField] int goldAmount;
    [SerializeField] int diamondAmount;
    [SerializeField] int goldEarningPerSecond;
    [SerializeField] int diamondEarningPerSecond;
    public bool[,] BuildingMatrix => buildingLayout.toBool2D();

    public Sprite Sprite => sprite;

    public int GoldAmount => goldAmount;
    public int DiamondAmount => diamondAmount;
    public int GoldEarningPerSecond => goldEarningPerSecond;
    public int DiamondEarningPerSecond => diamondEarningPerSecond;
}
