using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveSystem : ScriptableObject
{
    [SerializeField] private BuildingSO[] buildings;
    
    public int GoldAmount
    {
        get
        {
            return PlayerPrefs.GetInt(nameof(GoldAmount), -1);
        }
        set
        {
            PlayerPrefs.SetInt(nameof(GoldAmount),value);
        }
    } 
    public int DiamondAmount
    {
        get
        {
            return PlayerPrefs.GetInt(nameof(DiamondAmount), -1);
        }
        set
        {
            PlayerPrefs.SetInt(nameof(DiamondAmount),value);
        }
    }
    public Vector3[] BuildingConstructPosition
    => PlayerPrefsX.GetVector3Array(nameof(BuildingConstructPosition), null); 
    
    public int[] BuildingIndexes
    =>  PlayerPrefsX.GetIntArray(nameof(BuildingIndexes), null); 
    
    public int[] BuildingRotations
    =>  PlayerPrefsX.GetIntArray(nameof(BuildingRotations), null);

    public void AddBuildingPosition(Vector3 value)
    {
        List<Vector3> list; 
        if (BuildingConstructPosition == null)
        {
            list = new List<Vector3>();
        }
        else
        { 
            list = Enumerable.ToList<Vector3>(BuildingConstructPosition);
        }
        list.Add(value);
        PlayerPrefsX.SetVector3Array(
            nameof(BuildingConstructPosition),
            Enumerable.ToArray<Vector3>(list)
        );
    }
    public void AddBuildingIndex(int value)
    {
        List<int> list; 
        if (BuildingIndexes == null)
        {
            list = new List<int>();
        }
        else
        { 
            list = Enumerable.ToList<int>(BuildingIndexes);
            
        }
        list.Add(value);
        PlayerPrefsX.SetIntArray(
            nameof(BuildingIndexes),
            Enumerable.ToArray<int>(list)
        );
    }
    public void AddBuildingRotation(int value)
    {
        List<int> list; 
        if (BuildingRotations == null)
        {
            list = new List<int>();
        }
        else
        { 
            list = Enumerable.ToList<int>(BuildingRotations);
            
        }
        list.Add(value);
        PlayerPrefsX.SetIntArray(
            nameof(BuildingRotations),
            Enumerable.ToArray<int>(list)
        );
    }

   
    
    public int getBuildingIndex(BuildingSO buıilding)
    {
        return Array.IndexOf(buildings, buıilding);
    }  
    public BuildingSO getBuildingSO(int buıildingIndex)
    {
        return buildings[buıildingIndex];
    } 
    
    
    
}
