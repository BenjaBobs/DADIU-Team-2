using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Grid {

    static Dictionary<int, Dictionary<int, GameObject>> grid;

    public static void Initialize(int sizeX, int sizeY)
    {
        grid = new Dictionary<int, Dictionary<int, GameObject>>();
        for (int gridPosX = 0; gridPosX < sizeX; gridPosX++)
        {
            grid.Add(gridPosX + 1, new Dictionary<int, GameObject>());
            for (int gridPosY = 0; gridPosY < sizeY; gridPosY++)
            {
                grid[gridPosX][gridPosY] = null;
            }
        }
    }

    public static void InsertToGrid(int x, int y, GameObject mole)
    {
        grid[x][y] = mole;
    }

    public static GameObject LookupGrid(int x, int y)
    {
        if (grid.ContainsKey(x) && grid[x].ContainsKey(y))
            return grid[x][y];
        else
            return null;
    }

    public static void RemoveFromGrid(int x, int y)
    {
        grid[x][y] = null;
    }
}
