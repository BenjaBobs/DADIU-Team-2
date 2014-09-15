using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public float maxFrequency = 20.0f;
	public float minFrequency = 5.0f;
	float currentFrequency = 0.0f;
	float timeSinceSpawn = 0.0f;
    [HideInInspector]
	public int posX;
    [HideInInspector]
	public int posY;
	public float nearbySpecialMultiplier = 20.0f;
	public int minNearbyActivate = 1;
	public int maxNearbyActivate = 5;

	static List<GameObject> prefabs;
    static bool isLoaded = false;
    public List<GameObject> childs;

	private Mole SpeedUpMole;

    public GameObject hole;
    [HideInInspector]
    public float holeOffset = 0.0f;
    [HideInInspector]
    public float moleOffset = 0.0f;

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

	public GameObject mole;

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
				int CurrentFreeezNum = GameObject.FindGameObjectsWithTag("Enemy_Freeez").Length;
				//int CurrentFreeezNum = 0;
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
		timeSinceSpawn += Settings.instance.GetDeltaTime() * GetSpawnRateMultiplier();
		if (timeSinceSpawn >= currentFrequency) {

            CreateHole();

			// Instantiate mole and set its parent
			CalculateChildren();
			mole = (GameObject)Instantiate(childs[Random.Range(0, childs.Count)], transform.position, transform.rotation);
			mole.transform.parent = gameObject.transform;
			mole.GetComponent<Mole>().UpdateGridPosition(posX, posY);

			timeSinceSpawn = 0.0f;
			CalculateFrequency ();
			int currentNearbyActivate = Random.Range (minNearbyActivate, maxNearbyActivate);
			for (int i = 0; i < currentNearbyActivate; i++)
				CheckNearbyActivate();
		}
	}

	void CalculateFrequency()
	{
		currentFrequency = Random.Range (minFrequency, maxFrequency);
	}

	private bool HasExplosionNearby()
	{
		for (int x = posX-1; x <= posX+1; x++)
			for (int y = posY-1; y <= posY+1; y++)
		{
			GameObject obj = Grid.LookupGrid(x, y);
			if (obj && obj != mole && obj.GetComponent<Explosion>())
			{
				return true;
			}
		}
		return false;
	}
	private bool HasElectroNearby(bool expandY)
	{
		int maxValue = (expandY ? Grid.GetMaxY() : Grid.GetMaxX());
		for (int i = 1; i <= maxValue; i++)
		{
			GameObject obj = (expandY ? Grid.LookupGrid(posX, i) : Grid.LookupGrid(i, posY));
			if (obj && obj != mole && obj.GetComponent<Elektro>())
			{
				return true;
			}
		}
		return false;
	}

	private float GetSpawnRateMultiplier()
	{
		if (mole)
			return 0.0f;

		float m = Settings.instance.GetDifficultySpawnRate ();

		if (SpeedUpMole && SpeedUpMole.IsDead ())
			SpeedUpMole = null;
		else if (SpeedUpMole)
			m *= nearbySpecialMultiplier;


		return m;
	}

	private void CheckNearbyActivate()
	{
		/*
		List<Mole> m = new List<Mole> ();
		for (int x = posX-1; x <= posX+1; x++)
			for (int y = posY-1; y <= posY+1; y++)
		{
			GameObject obj = Grid.LookupGrid(x, y);
			if (obj && obj != mole && obj.GetComponent<Explosion>())
			{
				return true;
			}
		}
		*/
		return;

	}

    private void CreateHole()
    {
        if (!hole)
        {
            GameObject holePrefab = Resources.Load<GameObject>("Prefabs/dirtHole");
            hole = (GameObject)Instantiate(holePrefab, transform.position, transform.rotation);
            hole.transform.parent = transform;
        }
    }
}
