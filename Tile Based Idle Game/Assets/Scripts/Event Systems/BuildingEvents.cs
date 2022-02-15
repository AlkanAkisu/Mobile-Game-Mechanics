using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEvents : MonoBehaviour
{
    private static BuildingEvents _instance;

    public static BuildingEvents Instance { get { return _instance; } }

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

    public static Action<Building> buildingSelected = delegate { };
    public static Action buildingDeselected = delegate { };
    public static Action<Tile[]> TilesChanged = delegate { };
    public static Action<BuildingSO> BuildingConstructed = delegate { };





}
