using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuildingLayout
{

    [System.Serializable]
    public struct rowData
    {
        public bool[] row;
    }

    public rowData[] rows = new rowData[3]; 


    public bool[,] toBool2D()
    {
        var bool2D = new bool[3, 3];
        for (int i = 0; i < rows.Length; i++)
        {
            var row = rows[i].row;
            for (int j = 0; j < row.Length; j++)
            {
                bool2D[i, j] = row[j];
            }
        }

        return bool2D;
    }
}
