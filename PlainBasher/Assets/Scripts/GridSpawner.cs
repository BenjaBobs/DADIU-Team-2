using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {
	public bool spawnGrid;
	public int sizeX;
	public int sizeY;
	public float spacing;
	GameObject spawnType;

	// Use this for initialization
	void Start () {
        spawnType = Resources.Load<GameObject>("Prefabs/Spawner");
        Grid.Initialize(sizeX, sizeY);
		for(int gridPosX = 0; gridPosX < sizeX; gridPosX ++){
			for(int gridPosY = 0; gridPosY < sizeY; gridPosY ++){
				Vector3 position = gameObject.transform.position;
				position.x += spacing * (gridPosX - sizeX / 2);
				position.z += spacing * (gridPosY - sizeY / 2);
				GameObject obj = (GameObject)Instantiate(spawnType,position,gameObject.transform.rotation);
				Spawner spawner = obj.GetComponent<Spawner>();
				spawner.posX = gridPosX+1;
				spawner.posY = gridPosY+1;
				spawner.transform.parent = gameObject.transform;
			}
		}
	}


	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
