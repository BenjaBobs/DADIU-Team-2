using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Grid {

    static Dictionary<int, Dictionary<int, GameObject>> grid;
    static int gridSizeY;
    static int gridSizeX;

    public static void Initialize(int sizeX, int sizeY)
    {
        if (grid != null)
        {
            /*
            foreach(KeyValuePair<int, Key)

            for (int i = 1; i < gridSizeX; i++)
            {
                for (int j = 1; j < gridSizeY; j++)
                {
                    Debug.Log(i + " " + j);
                    GameObject mole = grid[i][j].GetComponent<Spawner>().mole;
                    if (mole != null)
                        GameObject.DestroyImmediate(mole);
                }
            }
            */
        }

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

	public static Mole GetMole(int x, int y)
	{
		Spawner s = GetSpawner(x, y);
		if (!s)
			return null;
		GameObject go = s.mole;
		if (!go)
			return null;
		Mole m = go.GetComponent<Mole> ();
		if (!m)
			return null;
		return m;
	}

    public static Spawner GetSpawner(int x, int y)
	{
		GameObject g = LookupGrid (x, y);
		if (!g)
			return null;
		Spawner s = g.GetComponent<Spawner> ();
		if (!s)
			return null;
		return s;
	}

    public static GameObject LookupGrid(int x, int y)
    {
		if (x > gridSizeX || x <= 0)
						return null;
		if (y > gridSizeY || y <= 0)
			return null;

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
