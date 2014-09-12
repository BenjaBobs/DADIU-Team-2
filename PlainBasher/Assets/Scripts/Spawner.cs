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

	List<Object> prefabs;
	List<Object> childs;

    GameObject mole;
	// Use this for initialization
	void Start () {
		CalculateFrequency ();
		timeSinceSpawn = Random.Range (0, currentFrequency);
		prefabs = new List<Object>(Resources.LoadAll ("Moles"));
		CalculateChildren ();
        
	}

	void CalculateChildren()
	{
		childs = new List<Object> ();
		foreach (Object prefab in prefabs) {
			int occurenceFactor = ((GameObject)prefab).GetComponent<Mole>().occurenceFactor;
			bool isJelly = ((GameObject)prefab).GetComponent<Jelly>() != null;

			if (!isJelly) occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultySpecialMoleMultiplier());

			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime * Settings.instance.GetDifficultySpeed();
		if (timeSinceSpawn >= currentFrequency) {
            if (mole != null)
                DestroyImmediate(mole);
			// Instantiate mole and set its parent

			CalculateChildren();
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
