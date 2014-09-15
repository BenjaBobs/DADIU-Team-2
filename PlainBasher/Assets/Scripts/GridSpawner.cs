using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Settings))]
public class GridSpawner : MonoBehaviour {

    public static GridSpawner staticRef;
    
	public int sizeX;
	public int sizeY;
	public float spacing;
    public bool preSpawnHoles = false;
    public float holeOffset = 0.0f;
    public float moleOffset = 0.0f;
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
                position.y += moleOffset;
                GameObject obj = (GameObject)Instantiate(spawnType, position, gameObject.transform.rotation);
                Spawner spawner = obj.GetComponent<Spawner>();
                spawner.posX = gridPosX + 1;
                spawner.posY = gridPosY + 1;
                spawner.holeOffset = holeOffset;
                spawner.moleOffset = moleOffset;
				Grid.InsertToGrid(spawner.posX, spawner.posY, obj);
                spawner.transform.parent = gameObject.transform;
                if (preSpawnHoles)
                {
                    Vector3 holePosition = position;
                    holePosition.y += holeOffset - moleOffset;
                    spawner.hole = SpawnHole(holePosition, gameObject.transform.rotation);
                }
            }
        }
    }

    GameObject SpawnHole(Vector3 holePosition, Quaternion rotation)
    {
        if (!hole)
        {
            hole = Resources.Load<GameObject>("Prefabs/dirtHole");
        }
        GameObject obj = (GameObject)Instantiate(hole, holePosition, rotation);
        obj.transform.parent = gameObject.transform;
        return obj;
    }
}
