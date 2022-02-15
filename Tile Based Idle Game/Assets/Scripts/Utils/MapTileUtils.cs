using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapTileUtils
{


    public static bool[,] BuildOnMatrix(int row, int column, bool[,] buildingMatrix, (int, int) startPoint)
    {
        return BuildOnMatrix(new bool[row, column], buildingMatrix, startPoint);
    }
    public static bool[,] BuildOnMatrix(bool[,] map, bool[,] buildingMatrix, (int, int) startPoint)
    {
        var output = map;
        var (y_Map, x_Map) = startPoint;
        var (x, y) = (0, 0);
        var (buildingRows, buildingColumns) = (buildingMatrix.GetLength(0), buildingMatrix.GetLength(1));

        while (y < buildingColumns)
        {
            bool bool_val = buildingMatrix[y, x];
            if (bool_val)
                output[y_Map, x_Map] = bool_val;

            x++;
            x_Map++;


            if (x == buildingRows)
            {
                x = 0;
                x_Map = startPoint.Item2;
                y++;
                y_Map++;
            }
        }


        return output;
    }



    public static bool[,] BuildMatrixByCurrent(int row, int column, bool[,] buildingMatrix, (int, int) UpLeftMostPointerLocation)
    {
        return BuildMatrixByCurrent(new bool[row, column], buildingMatrix, UpLeftMostPointerLocation);
    }
    public static bool[,] BuildMatrixByCurrent(bool[,] map, bool[,] buildingMatrix, (int, int) UpLeftMostPointerLocation)
    {
        return BuildMatrixByCurrentAndRotation(map, buildingMatrix, UpLeftMostPointerLocation, 0);
    }


    public static bool[,] BuildMatrixByCurrentAndRotation(int row, int column, bool[,] buildingMatrix,
        (int, int) UpLeftMostPointerLocation, int rotationTime)
    {
        return BuildMatrixByCurrentAndRotation(new bool[row, column], buildingMatrix, UpLeftMostPointerLocation, rotationTime);
    }
    public static bool[,] BuildMatrixByCurrentAndRotation(bool[,] map, bool[,] buildingMatrix,
        (int, int) UpLeftMostPointerLocation, int rotationTime)
    {
        var FTV_Point = FindFirstTrueValue(buildingMatrix);
        var N = buildingMatrix.GetLength(0);

        RotatePointAndMatrix(ref buildingMatrix, rotationTime, ref FTV_Point, N);

        (int, int) newStartPoint = TranslatePointerPosByFTV(UpLeftMostPointerLocation, FTV_Point);

        return BuildOnMatrix(map, buildingMatrix, newStartPoint);

    }

    private static (int, int) TranslatePointerPosByFTV((int, int) UpLeftMostPointerLocation, (int, int) FTV_Point)
    {
        var (y, x) = UpLeftMostPointerLocation;
        var (i, j) = FTV_Point;
        var newStartPoint = (y - i, x - j);
        return newStartPoint;
    }

    private static void RotatePointAndMatrix(ref bool[,] buildingMatrix, int rotationTime, ref (int, int) FTV_Point, int N)
    {
        for (int i = 0; i < rotationTime; i++)
        {
            FTV_Point = Rotate90CW(FTV_Point, N);
            buildingMatrix = RotateArrayCW(buildingMatrix);
        }
    }

    internal static object BuildMatrixByCurrentAndRotation(int v1, int v2, bool[,] buildingMatrix, (int, int) coordinate, object rotationTime)
    {
        throw new System.NotImplementedException();
    }

    public static bool CheckIfBuiltable(int rows, int columns, bool[,] buildingMatrix,
       (int, int) UpLeftMostPointerLocation, int rotationTime)
    {
        return CheckIfBuiltable(new bool[rows, columns], buildingMatrix, UpLeftMostPointerLocation, rotationTime);
    }

    public static bool CheckIfBuiltable(bool[,] map, bool[,] buildingMatrix,
        (int, int) UpLeftMostPointerLocation, int rotationTime)
    {

        // INITIALIZE
        var FTV_Point = FindFirstTrueValue(buildingMatrix);
        var N = buildingMatrix.GetLength(0);

        RotatePointAndMatrix(ref buildingMatrix, rotationTime, ref FTV_Point, N);
        (int, int) newStartPoint = TranslatePointerPosByFTV(UpLeftMostPointerLocation, FTV_Point);


        // LOGIC
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                var isBuildingOnIt = buildingMatrix[i, j];
                var thisPointOnMap = checkIfMapContainsPoint(map, (newStartPoint.Item1 + i, newStartPoint.Item2 + j));

                if (!thisPointOnMap && isBuildingOnIt)
                    return false;
            }

        }

        // (int, int) rightCorner = (newStartPoint.Item1 + N - 1, newStartPoint.Item2 + N - 1);
        // checkIfMapContainsPoint(map, rightCorner);
        return true;

    }

    private static bool checkIfMapContainsPoint(bool[,] map, (int, int) point)
    {
        if (point.Item1 < 0 || point.Item2 < 0)
            return false;
        return (map.GetLength(0) > point.Item1 && map.GetLength(1) > point.Item2);
    }

    public static (int, int) FindFirstTrueValue(bool[,] boolMatrix)
    {
        // Find the first true value from r->l up -> bottom
        var (rows, columns) = (boolMatrix.GetLength(0), boolMatrix.GetLength(1));
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (boolMatrix[i, j])
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    public static (int, int) Rotate90CW((int, int) point, int N)
    {
        var (i, j) = point;
        return (j, N - i - 1);
    }
    private static bool[,] RotateArrayCW(bool[,] src)
    {
        int width;
        int height;
        bool[,] dst;

        width = src.GetLength(0);
        height = src.GetLength(1);
        dst = new bool[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var (newCol, newRow) = Rotate90CW((col, row), width);

                dst[newCol, newRow] = src[col, row];
            }
        }

        return dst;
    }


    public static List<T> MaskWith<T>(T[,] mainArr, bool[,] mask)
    {
        List<T> list = new List<T>();

        var (rows, columns) = (mainArr.GetLength(0), mainArr.GetLength(1));
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (mask[i, j])
                    list.Add(mainArr[i, j]);
            }
        }
        return list;
    }
}

