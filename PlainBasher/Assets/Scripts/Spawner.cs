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

    #region Debugging
    [HideInInspector]
    public static Spawner DBGstaticRef;
    [HideInInspector]
    public float DBGjellyChance = 0;
    [HideInInspector]
    public float DBGfreeezChance = 0;
    [HideInInspector]
    public float DBGelektroChance = 0;
    [HideInInspector]
    public float DBGexplosionChance = 0;

    int DBGjellyCount = 0;
    int DBGfreeezCount = 0;
    int DBGelektroCount = 0;
    int DBGexplosionCount = 0;

    [HideInInspector]
    public bool Debugging = false;

    public void Debug()
    {
        Debugging = true;
        CalculateChildren();
        Debugging = false;

        int total = DBGjellyCount + DBGfreeezCount + DBGelektroCount + DBGexplosionCount;

        DBGjellyChance = DBGjellyCount / (float)total;
        DBGfreeezChance = DBGfreeezCount / (float)total;
        DBGelektroChance = DBGelektroCount / (float)total;
        DBGexplosionChance = DBGexplosionCount / (float)total;
    }

    void Awake()
    {
        DBGstaticRef = this;
    }
    #endregion

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

			if (prefab.GetComponent<Jelly>()) occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultyJelliesMultiplier());
			else if (prefab.GetComponent<Elektro>()) occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultyElectroMultiplier());
			else if (prefab.GetComponent<Explosion>()) occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultyExplodeMultiplier());
			else if (prefab.GetComponent<Freeez>())
			{
				occurenceFactor = (int)(occurenceFactor * Settings.instance.GetDifficultyFreezeMultiplier());
				int CurrentFreeezNum = GameObject.FindGameObjectsWithTag("Freeez").Length;
				if (CurrentFreeezNum >= Settings.instance.MaxFreeezAtOnce)
					occurenceFactor = 0;
			}

			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);

                #region Debugging
                if (Debugging)
                {
					if (prefab.GetComponent<Jelly>())
                        DBGjellyCount++;
                    else if (prefab.GetComponent<Freeez>() != null)
                        DBGfreeezCount++;
                    else if (prefab.GetComponent<Elektro>() != null)
                        DBGelektroCount++;
                    else if (prefab.GetComponent<Explosion>() != null)
                        DBGexplosionCount++;
                }
                #endregion
            }


		}

	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Settings.instance.GetDeltaTime() * Settings.instance.GetDifficultySpawnRate();
		if (timeSinceSpawn >= currentFrequency) {
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
