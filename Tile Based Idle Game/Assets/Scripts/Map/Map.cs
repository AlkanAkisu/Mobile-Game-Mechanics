using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Map : MonoBehaviour
{
    Transform[,] transformMatrix;
    bool[,] constructedBuildingMatrix;
    Vector3 initialCoordinate;
    [SerializeField] bool debugEnabled;

    public Transform[,] TransformMatrix { get => transformMatrix; set => transformMatrix = value; }

    private void Awake()
    {
        InitMap();
        constructedBuildingMatrix = new bool[10, 10];
    }

    private void Update()
    {
        DebugTools();
    }
    
    public void InitMap()
    {
        var firstChild = transform.GetChild(0).GetChild(0);
        var initialX = firstChild.transform.position.x - 0.5f * transform.localScale.x;
        var initialY = firstChild.transform.position.y - 0.5f * transform.localScale.y;
        initialCoordinate = new Vector3(initialX, initialY, 0);

        var N = transform.childCount;
        var M = transform.GetChild(0).childCount;
        transformMatrix = new Transform[N, M];

        LoopElements((tr, i, j) => transformMatrix[i, j] = tr);

    }


    public (int, int) FindCoordinateOfMouse()
    {
        var cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return FindCoordinateOfVector(cameraPos);

    } 
    public (int, int) FindCoordinateOfVector(Vector3 pos)
    {
        var distanceLeftTop = pos - initialCoordinate;
        var coordinate = (
            -Mathf.FloorToInt(distanceLeftTop.y ),
            Mathf.FloorToInt(distanceLeftTop.x ));

        return coordinate;

    }
    
    

    public void ConstructBuilding(Building currentBuilding, (int, int) lastBuildableMouseCoordinate)
    {
        Debug.Log($"Constructing {currentBuilding.BuildingStat.name} at {lastBuildableMouseCoordinate}");

        var boolMap = GetBoolMap(currentBuilding, lastBuildableMouseCoordinate);

        constructedBuildingMatrix = GetIntersect(constructedBuildingMatrix, boolMap);

        BuildingEvents.BuildingConstructed?.Invoke(currentBuilding.BuildingStat);

    }



    public bool CanBuild(Building currentBuilding, (int, int) lastBuildableMouseCoordinate)
    {
        var boolMap = GetBoolMap(currentBuilding, lastBuildableMouseCoordinate);

        return !IsIntersects(boolMap, constructedBuildingMatrix);
    }

    public bool IsIntersects(bool[,] matrix1, bool[,] matrix2)
    {
        var N = matrix1.GetLength(0);
        var M = matrix1.GetLength(1);
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (matrix1[i, j] && matrix2[i, j])
                    return true;
            }
        }
        return false;
    }

    public bool[,] GetIntersect(bool[,] matrix1, bool[,] matrix2)
    {
        var N = matrix1.GetLength(0);
        var M = matrix1.GetLength(1);
        bool[,] output = new bool[N, M];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                output[i, j] = matrix1[i, j] || matrix2[i, j];
            }
        }
        return output;
    }



    #region Utils

    private static bool[,] GetBoolMap(Building currentBuilding, (int, int) lastBuildableMouseCoordinate)
    {
        return MapTileUtils.BuildMatrixByCurrentAndRotation(
            10,
            10,
            currentBuilding.BuildingStat.BuildingMatrix,
            lastBuildableMouseCoordinate,
            currentBuilding.RotateTime
            );
    }

    private void DebugTools()
    {
        if (!debugEnabled)
            return;

        if (Input.GetKeyDown(KeyCode.C))
            Debug.Log($"{Camera.main.ScreenToWorldPoint(Input.mousePosition)}"); ;

        if (Input.GetKeyDown(KeyCode.S))
        {
            var coordinate = FindCoordinateOfMouse();
            Debug.Log($"Selected Coordinate: [{coordinate.Item1},{coordinate.Item2}]");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var coord = GetCoordinate(FindCoordinateOfMouse());
            Debug.Log($"{coord}");
        }
    }


    public void LoopElements(Action<Transform, int, int> callback)
    {
        var N = transform.childCount;
        var M = transform.GetChild(0).childCount;

        for (int i = 0; i < N; i++)
        {
            var row = transform.GetChild(i);
            for (int j = 0; j < M; j++)
            {
                var elem = row.GetChild(j);
                callback(elem, i, j);
            }
        }
    }

    public Vector3 GetCoordinate((int, int) index)
    {
        float lengthOfTile = 1;
        Vector3 firstPosition = transform.GetChild(0).GetChild(0).transform.position;
        var (x, y, _) = firstPosition.Deconstruct();
        var (i, j) = index;


        return new Vector3(x + j * lengthOfTile, y - i * lengthOfTile);

    }

    private T GetTileComponent<T>((int, int) coordinate) => transformMatrix[coordinate.Item1, coordinate.Item2].GetComponent<T>();
    #endregion

}
