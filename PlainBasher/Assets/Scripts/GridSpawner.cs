using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Settings))]
public class GridSpawner : MonoBehaviour {

    public static GridSpawner staticRef;
    
	public int sizeX;
	public int sizeY;
	public float spacing;
    public bool spawnHoles = false;
	GameObject spawnType;
    GameObject hole;

    void Awake()
    {
        staticRef = this;
    }

	// Use this for initialization
	void Start () {
        spawnType = Resources.Load<GameObject>("Prefabs/Spawner");
        ResetGrid();
	}

    public void ResetGrid()
    {
        Grid.Initialize(sizeX, sizeY);
        for (int gridPosX = 0; gridPosX < sizeX; gridPosX++)
        {
            for (int gridPosY = 0; gridPosY < sizeY; gridPosY++)
            {
                Vector3 position = gameObject.transform.position;
                position.x += spacing * (gridPosX - sizeX / 2);
                position.z += spacing * (gridPosY - sizeY / 2);
                GameObject obj = (GameObject)Instantiate(spawnType, position, gameObject.transform.rotation);
                Spawner spawner = obj.GetComponent<Spawner>();
                spawner.posX = gridPosX + 1;
                spawner.posY = gridPosY + 1;
                spawner.transform.parent = gameObject.transform;
                if (spawnHoles)
                {
                    Vector3 holePosition = position;
                    holePosition.y = 0.1f;
                    SpawnHole(hole, holePosition, gameObject.transform.rotation);
                }
            }
        }
    }

    void SpawnHole(GameObject hole, Vector3 holePosition, Quaternion rotation)
    {
        if (!hole)
        {
            hole = Resources.Load<GameObject>("Prefabs/dirtHole");
        }
        GameObject obj = (GameObject)Instantiate(hole, holePosition, rotation);
        obj.transform.parent = gameObject.transform;
    }
}
