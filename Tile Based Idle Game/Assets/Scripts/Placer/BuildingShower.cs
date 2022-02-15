using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingShower
{
    private Map map;
    private int lastRotationTime;

    public BuildingShower(Map map)
    {
        this.map = map;
        lastRotationTime = 0;
    }


    public void ShowBuilding(Building building, (int, int) coordinate, int rotationTime)
    {
        if (map.CanBuild(building, coordinate))
            building.BuildableColor();
        else
            building.NonBuildableColor();
        var position = CenterPointBuildingMatrix(building.buildingMatrix, coordinate, rotationTime);

        if (rotationTime == lastRotationTime)
            building.transform.position = position;
        else
            building.GoPosition(position);
        building.Rotate(rotationTime);
        lastRotationTime = rotationTime;
    }

    public Vector3 CenterPointBuildingMatrix(bool[,] buildingMatrix, (int, int) coordinate, int rotationTime)
    {
        var mask = MapTileUtils.BuildMatrixByCurrentAndRotation(10, 10, buildingMatrix, coordinate, rotationTime);

        var centerPoint = AverageOfAllPoints(mask);

        return centerPoint;
    }

    private Vector3 AverageOfAllPoints(bool[,] mask)
    {
        var N = mask.GetLength(0);
        List<Vector3> coordinates = new List<Vector3>();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (mask[i, j])
                    coordinates.Add(map.GetCoordinate((i, j)));
            }
        }
        var sumVector = Vector3.zero;
        coordinates.ForEach(vect => sumVector += vect);
        return sumVector / coordinates.Count;
    }


}
