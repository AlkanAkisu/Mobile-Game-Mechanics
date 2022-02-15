using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;
    private Placer placer;
    [SerializeField] private SaveSystem _saveSystem;

    private void Awake()
    {
        placer = FindObjectOfType<Placer>();
    }
    
    
    
    public void CreateBuilding(BuildingSO stat)
    {
        var building = Instantiate(buildingPrefab, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        building.BuildingStat = stat;
        building.Init();
        placer.CurrentBuilding = building;

    }
}
