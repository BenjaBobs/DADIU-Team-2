using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {
	public bool spawnGrid;
	public int sizeX;
	public int sizeY;
	public float spacing;
	public Transform spawnType;
	Dictionary<int, Dictionary<int, GameObject>> grid;

	// Use this for initialization
	void Start () {
		grid = new Dictionary<int, Dictionary<int, GameObject>> ();
		for(int x = 0; x < sizeX; x ++){
			grid.Add(x+1, new Dictionary<int, GameObject>());
			for(int y = 0; y < sizeY; y ++){
				Vector3 position = gameObject.transform.position;
				position.x += spacing * (x - sizeX / 2);
				position.z += spacing * (y - sizeY / 2);
				GameObject obj = (GameObject)Instantiate(spawnType,position,gameObject.transform.rotation);
				Spawner spawner = obj.GetComponent<Spawner>();
				spawner.posX = x+1;
				spawner.posY = y+1;
				spawner.transform.parent = gameObject.transform;
			}
		}
	}

	public void InsertToGrid(int x, int y, GameObject mole)
	{
		grid [x] [y] = mole;
	}

	public GameObject LookupGrid(int x, int y)
	{
		if (grid.ContainsKey (x) && grid [x].ContainsKey (y))
			return grid [x] [y];
		else
			return null;
	}

	public void RemoveFromGrid(int x, int y)
	{
		grid [x][y] = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
