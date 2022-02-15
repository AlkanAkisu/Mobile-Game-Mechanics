using System.Collections.Generic;


public class MatchUtils
{
    private static List<(int, int)> visited;
    public static int CheckIfMatch3<T>(T id, (int, int) coordinate, T[,] matrix)
    {
        visited = new List<(int, int)>();
        return checkAdjancent<T>(id, coordinate, matrix, visited);
    }

    static int checkAdjancent<T>(T id, (int,int) coordinate, T[,] matrix, List<(int,int)> visited)
    {
        var (i, j) = coordinate;
        if (visited.Contains(coordinate))
            return 0;
        if (i < 0 || i >= matrix.GetLength(0))
            return 0;
        if(j < 0 || j >= matrix.GetLength(1))
            return 0;
        if (matrix[i, j] == null)
            return 0;
        if (!matrix[i, j].Equals(id))
            return 0;
        visited.Add(coordinate);
        return 1 + checkAdjancent(id, (i + 1, j), matrix,visited)
                 + checkAdjancent(id, (i - 1, j), matrix,visited)
                 + checkAdjancent(id, (i, j + 1), matrix,visited)
                 + checkAdjancent(id, (i, j - 1), matrix,visited);

    }

    public static List<(int, int)> GetMatches() => visited;
}