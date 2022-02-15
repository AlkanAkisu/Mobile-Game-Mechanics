using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] private Map map;
    [SerializeField] private Building currentBuilding;
    [SerializeField] private SaveSystem _saveSystem;

    #endregion

    #region Private Fields

    List<Tile> selectedTiles;
    (int, int) mouseCoordinates;
    private int rotationCounter;
    BuildingShower buildingShower;
    private BuildingCreator buildingCreator;
    private (int, int) lastBuildableMouseCoordinate;
    private bool buildingNow;
    #endregion

    #region Public Properties
    public bool[,] BuildingMatrix => CurrentBuildingSO?.BuildingMatrix;

    public BuildingSO CurrentBuildingSO => currentBuilding.BuildingStat;
    public Building CurrentBuilding
    {
        get => currentBuilding; set

        {
            currentBuilding = value;
            lastBuildableMouseCoordinate = (-1, -1);
            ResetRotationCounter();
        }
    }

    #endregion

    void Awake()
    { 
        buildingCreator = FindObjectOfType<BuildingCreator>();
        buildingNow = false;
    }

    void Start()
    {
        map = FindObjectOfType<Map>();
        mouseCoordinates = (-1, -1);
        selectedTiles = new List<Tile>();
        buildingShower = new BuildingShower(map);
        lastBuildableMouseCoordinate = (-1, -1);

        InitEvents();
        if(_saveSystem.BuildingIndexes == null)
            return;
        buildingNow = true;
        var indexes = _saveSystem.BuildingIndexes; 
        var rotations = _saveSystem.BuildingRotations; 
        var coordinates = _saveSystem.BuildingConstructPosition;
        for (int i = 0; i < indexes.Length; i++)
        {
            BuildingSO stat = _saveSystem.getBuildingSO(indexes[i]);
            buildingCreator.CreateBuilding(stat);

            lastBuildableMouseCoordinate = map.FindCoordinateOfVector(coordinates[i]);
            currentBuilding.Rotate(rotations[i],false);
            Debug.Log($"{coordinates[i]} {lastBuildableMouseCoordinate} rotation:{currentBuilding.RotateTime}");
            CurrentBuilding.transform.position = buildingShower.CenterPointBuildingMatrix(
                stat.BuildingMatrix,
                lastBuildableMouseCoordinate,
                rotations[i]
            ); 
            Construct();
            
            

        }
        buildingNow = false;
        if (currentBuilding != null)
        {
            CannotConstruct();
        }
    }

    private void InitEvents()
    {
        BuildingEvents.buildingSelected += (building) => CurrentBuilding = building;
        BuildingEvents.buildingDeselected += () => CurrentBuilding = null;
    }

    private void Update()
    {
        if (currentBuilding == null)
            return;
        bool isTileChanged = mouseCoordinates != map.FindCoordinateOfMouse();
        mouseCoordinates = map.FindCoordinateOfMouse();

        // LOGIC
        if (isTileChanged)
        {
            UpdateTile(BuildingMatrix);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IncreaseRotationCounter(BuildingMatrix);
            UpdateTile(BuildingMatrix);
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (lastBuildableMouseCoordinate.Item1 == -1)
                return;

            Construct();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CannotConstruct();
        }

        // --

    }

    private void Construct()
    {
        if (!map.CanBuild(currentBuilding, lastBuildableMouseCoordinate))
        {
            CannotConstruct();
            return;
        }
        currentBuilding.IsPlaced = true;
        map.ConstructBuilding(currentBuilding, lastBuildableMouseCoordinate);
        if (!buildingNow)
        {
            _saveSystem.AddBuildingIndex(_saveSystem.getBuildingIndex(currentBuilding.BuildingStat));
            _saveSystem.AddBuildingPosition(map.GetCoordinate(lastBuildableMouseCoordinate));
            _saveSystem.AddBuildingRotation(currentBuilding.RotateTime);
            Debug.Log($"{map.GetCoordinate(lastBuildableMouseCoordinate)} {lastBuildableMouseCoordinate} rotation:{currentBuilding.RotateTime}");
        }
        currentBuilding = null;
        MouseLeaves();
    }

    private void CannotConstruct()
    {
        Destroy(currentBuilding.gameObject);
        CurrentBuilding = null;
        MouseLeaves();
    }

    private void UpdateTile(bool[,] buildingMatrix)
    {
        var isBuildable = MapTileUtils.CheckIfBuiltable(10, 10, buildingMatrix, mouseCoordinates, rotationCounter);

        if (!isBuildable)
        {
            CannotBuild();
            return;
        }

        lastBuildableMouseCoordinate = mouseCoordinates;

        new TileSelector().
        NewTileSelected(
            ref selectedTiles,
            mouseCoordinates,
            buildingMatrix,
            map.TransformMatrix,
            rotationCounter
        );


        buildingShower.ShowBuilding(
            CurrentBuilding,
            mouseCoordinates,
            rotationCounter
        );

        BuildingEvents.TilesChanged?.Invoke(selectedTiles.ToArray());
    }

    public void CannotBuild()
    {
        var mousePos = map.FindCoordinateOfMouse();
        int size = map.TransformMatrix.GetLength(0);
        if (mousePos.Item1 < 0 || mousePos.Item2 < 0
            || mousePos.Item1 >= size || mousePos.Item2 >= size
            )
            MouseLeaves();
    }

    public void MouseLeaves()
    {
        selectedTiles?.ForEach(tile => tile.Deselect());
    }

    public void IncreaseRotationCounter(bool[,] buildingMatrix)
    {
        var isBuildable = MapTileUtils.CheckIfBuiltable(10, 10, buildingMatrix, mouseCoordinates, rotationCounter + 1);
        if (isBuildable)
            rotationCounter++;
    }
    public void ResetRotationCounter() => rotationCounter = 0;


}
