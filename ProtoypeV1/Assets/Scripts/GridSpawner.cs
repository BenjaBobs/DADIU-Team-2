using UnityEngine;
using System.Collections;

public class GridSpawner : MonoBehaviour {
	public bool spawnGrid;
	public int sizeX;
	public int sizeY;
	public float spacing;
	public Transform spawnType;
	// Use this for initialization
	void Start () {
		for(int x = 0; x < sizeX; x ++){
			for(int y = 0; y < sizeY; y ++){
				Vector3 position = gameObject.transform.position;
				position.x += spacing * (x - sizeX / 2);
				position.z += spacing * (y - sizeY / 2);
				Instantiate(spawnType,position,gameObject.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
