using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MapUtilsEMTest
{
    [Test]
    public void Check_If_Matrix_Spawn_Building_Correctly_2()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {false,true,false},
            {false,true,false}
        };
        var (row, column) = (10, 10);
        var output = MapTileUtils.BuildOnMatrix(row, column, buildingMatrix, (2, 1));
        var realOutput = new bool[10, 10]
        {
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,true,true,true,false,false,false,false,false,false},
            {false,false,true,false,false,false,false,false,false,false},
            {false,false,true,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
        };


        Assert.AreEqual(realOutput, output);

    }
    [Test]
    public void Check_If_Matrix_Spawn_Building_Correctly_1()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {true,false,false},
            {true,false,false}
        };
        var (row, column) = (10, 10);
        var output = MapTileUtils.BuildOnMatrix(row, column, buildingMatrix, (0, 0));
        var realOutput = new bool[10, 10]
        {
            {true,true,true,false,false,false,false,false,false,false},
            {true,false,false,false,false,false,false,false,false,false},
            {true,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
        };


        Assert.AreEqual(realOutput, output);

    }
    [Test]
    public void Check_If_Matrix_Pointer_Spawn_Building_Correctly_1()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {false,false,true},
            {false,false,true},
            {true,true,true}
        };
        var (row, column) = (10, 10);
        var output = MapTileUtils.BuildMatrixByCurrent(row, column, buildingMatrix, (0, 2));
        var realOutput = new bool[10, 10]
        {
            {false,false,true,false,false,false,false,false,false,false},
            {false,false,true,false,false,false,false,false,false,false},
            {true,true,true,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
        };




        Assert.AreEqual(realOutput, output);

    }
    [Test]
    public void Check_If_Matrix_Pointer_Spawn_Building_Correctly_2()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {false,true,true},
            {false,true,true},
            {true,true,true}
        };
        var (row, column) = (10, 10);
        var output = MapTileUtils.BuildMatrixByCurrent(row, column, buildingMatrix, (3, 6));
        var realOutput = new bool[10, 10]
        {
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,true,true,false,false},
            {false,false,false,false,false,false,true,true,false,false},
            {false,false,false,false,false,true,true,true,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
        };




        Assert.AreEqual(realOutput, output);

    }
    [Test]
    public void Check_If_Matrix_Pointer_And_Rotated_Spawn_Building_Correctly_2()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {true,false,false},
            {true,false,false}
        };
        var (row, column) = (10, 10);

        var output = MapTileUtils.BuildMatrixByCurrentAndRotation(row, column, buildingMatrix, (2, 3), 1);

        var realOutput = new bool[10, 10]
        {
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,true,true,true,false,false,false,false,false,false},
            {false,false,false,true,false,false,false,false,false,false},
            {false,false,false,true,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false,false,false},
        };
        Assert.AreEqual(realOutput, output);

    }

    [Test]
    public void Check_Mask_With()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {true,false,false},
            {true,false,false}
        };
        var (row, column) = (10, 10);

        var output = MapTileUtils.BuildMatrixByCurrentAndRotation(row, column, buildingMatrix, (2, 3), 1);

        var mainArr = new int[10, 10]
        {
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,12,45,67,1,1,1,1,1,1},
            {1,1,1,35,1,1,1,1,1,1},
            {1,1,1,98,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
        };
        var list = MapTileUtils.MaskWith(mainArr, output);
        Assert.Contains(12, list);
        Assert.Contains(45, list);
        Assert.Contains(67, list);
        Assert.Contains(35, list);
        Assert.Contains(98, list);

    }

    [Test]
    public void Check_If_Matrix_Is_Builtable()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {true,false,false},
            {true,false,false}
        };
        var (row, column) = (10, 10);

        bool isBuiltable = MapTileUtils.CheckIfBuiltable(row, column, buildingMatrix, (9, 3), 1);

        // {false,false,false,false,false,false,false,false,false, TRUE}, TRUE
        // {false,false,false,false,false,false,false,false,false,false}, TRUE
        // {false,false,false,false,false,false,false,false,false,false}, TRUE
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        Assert.IsFalse(isBuiltable);

    }

    [Test]
    public void Check_If_Matrix_Is_Builtable_2()
    {
        var buildingMatrix = new bool[3, 3]
        {
            {true,true,true},
            {true,false,false},
            {true,false,false}
        };
        var (row, column) = (10, 10);

        bool isBuiltable = MapTileUtils.CheckIfBuiltable(row, column, buildingMatrix, (0, 0), 3);


        // {TRUE,false,false,false,false,false,false,false,false, false}, 
        // {TRUE,false,false,false,false,false,false,false,false,false}, 
        // {TRUE,TRUE,TRUE,false,false,false,false,false,false,false}, 
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},
        // {false,false,false,false,false,false,false,false,false,false},

        Assert.IsTrue(isBuiltable);

    }


    private string MatrixToString<T>(T[,] arr)
    {
        var (rows, columns) = (arr.GetLength(0), arr.GetLength(1));
        var str = "[\n";
        for (int i = 0; i < rows; i++)
        {
            str += "[";
            for (int j = 0; j < columns; j++)
            {
                str += $"{arr[i, j]}";
                if (j + 1 != columns)
                    str += ",";
            }
            str += "]\n";
        }
        return str;
    }



}

