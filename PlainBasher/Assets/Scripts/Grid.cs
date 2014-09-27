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
		if (m.IsDead ())
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

    public static List<Spawner> GetSpawnerLine(int x, int y, bool horizontal)
    {
        List<Spawner> sList = new List<Spawner>();

        if (horizontal)
        {
            for (int i = 1; i <= GetMaxX(); i++)
            {
                sList.Add(GetSpawner(i, y));
            }
        }
        else
            for (int i = 1; i <= GetMaxY(); i++)
            {
                sList.Add(GetSpawner(x, i));
            }

        return sList;
    }

    public static List<Spawner> GetSpawnerRadius(int x, int y, int radius)
    {
        List<Spawner> sList = new List<Spawner>();

        for (int i = x-radius; i <= x+radius; i++)
        {
            for (int j = y-radius; j <= y+radius; j++)
            {
                Spawner s = GetSpawner(i,j);
                if (s != null && !sList.Contains(s))
                    sList.Add(s);

            }
        }

        return sList;
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

	public static void WipeBoard()
	{
		for (int x = 1; x <= gridSizeX; x++)
		{
			for (int y = 1; y <= gridSizeY; y++)
			{
                Spawner s = GetSpawner(x, y);
                s.NearbyElektros = 0;
                s.NearbyExplosions = 0;

				Mole m = GetMole (x, y);
				if (m)
				    if (!m.IsDead ())
				        m.OnDeath (false);

                
			}
		}
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
