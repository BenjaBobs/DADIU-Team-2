using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public float maxFrequency = 10.0f;
	public float minFrequency = 2.0f;
	float currentFrequency = 0.0f;
	float timeSinceSpawn = 0.0f;
    [HideInInspector]
	public int posX;
    [HideInInspector]
	public int posY;

	static List<GameObject> prefabs;
    List<GameObject> childs;

    GameObject mole;
	// Use this for initialization
	void Start () {
		CalculateFrequency ();
		timeSinceSpawn = Random.Range (0, currentFrequency);
		prefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Moles"));
        childs = new List<GameObject>();
        foreach (GameObject prefab in prefabs)
        {
			int occurenceFactor = prefab.GetComponent<Mole>().occurenceFactor;
			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);	
	        }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime * Settings.staticRef.difficultyMultiplier;
		if (timeSinceSpawn >= currentFrequency) {
            if (mole != null)
                DestroyImmediate(mole);
			// Instantiate mole and set its parent

			mole = (GameObject)Instantiate(childs[Random.Range(0, childs.Count)], transform.position, transform.rotation);
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
