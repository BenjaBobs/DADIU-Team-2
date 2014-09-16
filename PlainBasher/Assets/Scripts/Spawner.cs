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
    public bool isSpawning = true;
    public Quaternion moleRotation = Quaternion.Euler(0, 90, 0);

	static List<GameObject> prefabs;
    static bool isLoaded = false;
    public List<GameObject> childs;

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
			Mole m = prefab.GetComponent<Mole>();
			if (!m) continue;
			int occurenceFactor = m.occurenceFactor;

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
		if (timeSinceSpawn >= currentFrequency && isSpawning == true) {
			// Instantiate mole and set its parent
			PlaceMole();
		}
	}

	public void PlaceMole()
	{
		CalculateChildren();
		PlaceMole(childs[Random.Range(0, childs.Count)]);
	}

	public void PlaceMole(GameObject type)
	{
		if (mole)
			return;

		CreateHole();

		hole.GetComponent<Hole>().DisplayParticles();
		mole = (GameObject)Instantiate(type, transform.position, moleRotation);
		mole.transform.parent = gameObject.transform;
		mole.GetComponent<Mole>().UpdateGridPosition(posX, posY);

		timeSinceSpawn = 0.0f;
		CalculateFrequency ();
		int currentNearbyActivate = Random.Range (minNearbyActivate, maxNearbyActivate);
		for (int i = 0; i < currentNearbyActivate; i++)
			CheckNearbyActivate();
	}
	
	void CalculateFrequency()
	{
		currentFrequency = Random.Range (minFrequency, maxFrequency);
	}

	private Spawner GetRandomWithinExplosion()
	{
		List<Spawner> m = new List<Spawner> ();
		for (int x = posX-1; x <= posX+1; x++)
			for (int y = posY-1; y <= posY+1; y++)
		{
			Spawner obj = Grid.GetSpawner(x, y);
			if (!obj) continue;
			if (obj.gameObject == gameObject) continue;
			if (obj.mole) continue;
			m.Add (obj);
		}
		if (m.Count == 0)
			return null;
		return m[Random.Range (0,m.Count)];
	}
	private Spawner GetRandomWithinElectro()
	{
		List<Spawner> result = new List<Spawner> ();

		List<Spawner> i = GetListWithinElectro (false);
		foreach (Spawner m in i)
			result.Add (m);

		i = GetListWithinElectro (false);
		foreach (Spawner m in i)
			result.Add (m);

		if (result.Count == 0)
			return null;
		return result[Random.Range (0,result.Count)];
	}

	private List<Spawner> GetListWithinElectro(bool expandY)
	{
		List<Spawner> m = new List<Spawner> ();
		int maxValue = (expandY ? Grid.GetMaxY() : Grid.GetMaxX());
		for (int i = 1; i <= maxValue; i++)
		{
			Spawner obj = (expandY ? Grid.GetSpawner(posX, i) : Grid.GetSpawner(i, posY));
			if (!obj) continue;
			if (obj.gameObject == gameObject) continue;
			if (obj.mole) continue;
			m.Add (obj);
		}
		return m;
	}

	private float GetSpawnRateMultiplier()
	{
		if (mole)
			return 0.0f;

		float m = Settings.instance.GetDifficultySpawnRate ();
		return m;
	}

	private void CheckNearbyActivate()
	{
		if (!mole)
			return;
		Mole m = mole.GetComponent<Mole> ();
		if (!m)
			return;
		if (mole.GetComponent<Elektro> ())
		{
			Spawner s = GetRandomWithinElectro();
			if (s)
			{
				s.PlaceMole();
			}
		}
		if (mole.GetComponent<Explosion> ())
		{
			Spawner s = GetRandomWithinExplosion();
			if (s)
			{
				s.PlaceMole();
			}
		}

		return;

	}

    public void CreateHole()
    {
        if (!hole)
        {
            GameObject holePrefab = Resources.Load<GameObject>("Prefabs/dirtHole");
            hole = (GameObject)Instantiate(holePrefab, transform.position, transform.rotation);
            hole.transform.parent = transform;
        }
    }

}
