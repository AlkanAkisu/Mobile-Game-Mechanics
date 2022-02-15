using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSelector
{
    public List<T> SelectTiles<T>((int, int) mouseCoordinate, bool[,] buildingMatrix, Transform[,] transformMatrix, int rotationTime)
    {

        var coordinate = mouseCoordinate;
        if (!MapTileUtils.CheckIfBuiltable(10, 10, buildingMatrix, coordinate, rotationTime))
        {
            Debug.Log($"Cannot build there");
            return null;
        }
        var mask = MapTileUtils.BuildMatrixByCurrentAndRotation(10, 10, buildingMatrix, coordinate, rotationTime);
        var selectedTiles = MapTileUtils.MaskWith(transformMatrix, mask);

        return selectedTiles.Select((tranform) => tranform.GetComponent<T>()).ToList();
    }


    public List<T> SelectTiles<T>((int, int) mouseCoordinate, bool[,] buildingMatrix, Transform[,] transformMatrix)
    {
        return SelectTiles<T>(mouseCoordinate, buildingMatrix, transformMatrix, 0);
    }


    public void NewTileSelected(ref List<Tile> selectedTiles,
        (int, int) mouseCoordinates, bool[,] buildingMatrix, Transform[,] map, int rotationTime)
    {
        DeselectTiles(selectedTiles);

        selectedTiles = NewTileSelected(mouseCoordinates, buildingMatrix, map, rotationTime);

    }


    public List<Tile> NewTileSelected((int, int) mouseCoordinates, bool[,] buildingMatrix, Transform[,] map, int rotationTime)
    {
        var selectedTiles = SelectTiles<Tile>(mouseCoordinates, buildingMatrix, map, rotationTime);

        selectedTiles?.ForEach(tile => tile.Select());

        return selectedTiles;
    }


    public void DeselectTiles(List<Tile> selectedTiles)
    {
        selectedTiles.ForEach(tile => tile.Deselect());

    }


}
