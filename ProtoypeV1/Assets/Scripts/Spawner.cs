using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	GameObject player;
	public float maxFrequency = 10.0f;
	public float minFrequency = 2.0f;
	float currentFrequency = 0.0f;
	float timeSinceSpawn = 0.0f;
	//public Transform child;
	List<Object> prefabs;
	List<Object> childs;
	// Use this for initialization
	void Start () {
		CalculateFrequency ();
		timeSinceSpawn = Random.Range (0, currentFrequency);
		player = GameObject.Find("Player");
		prefabs = new List<Object>(Resources.LoadAll ("Moles"));
		childs = new List<Object> ();
		foreach (Object prefab in prefabs) {
			int occurenceFactor = ((GameObject)prefab).GetComponent<Mole>().occurenceFactor;
			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);	
	        }
        }
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime * ((Player)player.gameObject.GetComponent(typeof(Player))).difficultyMultiplier;
		if (timeSinceSpawn >= currentFrequency) {
			// Instantiate mole and set its parent
			GameObject mole = (GameObject)Instantiate(childs[Random.Range(0, childs.Count)], transform.position, transform.rotation);
			mole.transform.parent = gameObject.transform;
			timeSinceSpawn = 0.0f;
			CalculateFrequency ();
		}
	}

	void CalculateFrequency()
	{
		currentFrequency = Random.Range (minFrequency, maxFrequency);
	}
}
