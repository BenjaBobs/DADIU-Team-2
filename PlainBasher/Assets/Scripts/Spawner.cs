using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public float maxFrequency = 50.0f;
	public float minFrequency = 20.0f;
	float currentFrequency = 0.0f;
	float timeSinceSpawn = 0.0f;
    [HideInInspector]
	public int posX;
    [HideInInspector]
	public int posY;

	static List<GameObject> prefabs;
    static bool isLoaded = false;
    public List<GameObject> childs;

    public GameObject mole;
	// Use this for initialization
	void Start () {
		CalculateFrequency ();
		timeSinceSpawn = Random.Range (0, currentFrequency);

        if (!isLoaded)
        {
		    prefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Moles"));
            isLoaded = true;
        }

		CalculateChildren ();
        
	}

	void CalculateChildren()
	{
		childs = new List<GameObject> ();
		foreach (GameObject prefab in prefabs) {
			int occurenceFactor = prefab.GetComponent<Mole>().occurenceFactor;
			bool isJelly = prefab.GetComponent<Jelly>() != null;

			if (!isJelly) occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultySpecialMoleMultiplier());

			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Settings.instance.GetDeltaTime() * Settings.instance.GetDifficultySpeed();
		if (timeSinceSpawn >= currentFrequency) {
            if (mole != null)
                DestroyImmediate(mole);
			// Instantiate mole and set its parent

			CalculateChildren();
			mole = (GameObject)Instantiate(childs[Random.Range(0, childs.Count)], transform.position, transform.rotation);
			mole.transform.parent = gameObject.transform;
			mole.GetComponent<Mole>().UpdateGridPosition(posX, posY);

			timeSinceSpawn = 0.0f;
			CalculateFrequency ();
		}
	}

	void CalculateFrequency()
	{
		currentFrequency = Random.Range (minFrequency, maxFrequency);
	}
}
