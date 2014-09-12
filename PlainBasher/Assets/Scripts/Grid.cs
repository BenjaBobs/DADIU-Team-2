using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Grid {

    static Dictionary<int, Dictionary<int, GameObject>> grid;
    static int gridSizeY;
    static int gridSizeX;

    public static void Initialize(int sizeX, int sizeY)
    {
        grid = new Dictionary<int, Dictionary<int, GameObject>>();
        for (int gridPosX = 1; gridPosX <= sizeX; gridPosX++)
        {
            grid.Add(gridPosX, new Dictionary<int, GameObject>());
            for (int gridPosY = 1; gridPosY <= sizeY; gridPosY++)
            {
                grid[gridPosX].Add(gridPosY, null);
            }
        }
        gridSizeX = sizeX;
        gridSizeY = sizeY;
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

    public static int GetMaxX()
    {
        return gridSizeX;
    }

    public static int GetMaxY()
    {
        return gridSizeY;
    }
}
