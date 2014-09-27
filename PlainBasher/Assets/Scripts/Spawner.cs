using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public float maxFrequency = 20.0f;
	public float minFrequency = 5.0f;
	float currentFrequency = 0.0f;
	float timeSinceSpawn = 0.0f;
    
	public int posX;
    
	public int posY;
	public float nearbySpecialMultiplier = 20.0f;
	public int minNearbyActivate = 1;
	public int maxNearbyActivate = 5;
    public bool isSpawning = true;
    private Quaternion moleRotation = Quaternion.Euler(0, 180, 0);

	static List<GameObject> prefabs;
    static bool isLoaded = false;
    public List<GameObject> childs;

    public GameObject hole;
    [HideInInspector]
    public float holeOffset = 0.0f;
    [HideInInspector]
    public float moleOffset = 0.0f;

    [SerializeField]
    private AoEIndicator aoeIndicator;

    [SerializeField]
    private int nearbyExplosions = 0;
    [SerializeField]
    private int nearbyElektros = 0;

    public int NearbyElektros
    {
        get { return nearbyElektros; }
        set 
        { 
            if (value <= 0)
            {
                aoeIndicator.RemoveElektro();
                value = 0;
            }
            else
            {
                aoeIndicator.IndicateElektro();
            }

            nearbyElektros = value;
        }
    }

    public int NearbyExplosions
    {
        get { return nearbyExplosions; }
        set 
        { 
            if (value <= 0)
            {
                aoeIndicator.RemoveExplosion();
                value = 0;
            }
            else
            {
                aoeIndicator.IndicateExplosion();
            }
            nearbyExplosions = value;
        }
    }

    #region Debugging
    [HideInInspector]
    public static Spawner DBGstaticRef;
    [HideInInspector]
	public GameObject mole;

    [HideInInspector]
    public bool Debugging = false;

    void Awake()
    {
        DBGstaticRef = this;
    }
    #endregion

	// Use this for initialization
    void Start()
    {
		CalculateFrequency ();
		timeSinceSpawn = Random.Range (0, currentFrequency);

		CalculateChildren ();
        
	}

	void CalculateChildren()
	{
		childs = new List<GameObject> ();
		if (prefabs == null)
			prefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Moles"));

		foreach (GameObject prefab in prefabs)
		{
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
				if (CurrentFreeezNum >= Settings.instance.MaxFreeezAtOnce)
					occurenceFactor = 0;
			}

			for (int i = 0; i <= occurenceFactor; i++)
			{
				childs.Add(prefab);

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

	public void PlaceJelly()
	{
		CalculateChildren();
		GameObject jellyPrefab = null;
		for (int i = 0; i < childs.Count; i++)
		{
			if (!childs[i].GetComponent<Jelly>())
				continue;
			jellyPrefab = childs[i];
			break;
		}
		if (jellyPrefab)
			PlaceMole (jellyPrefab);
		else
			PlaceMole ();
	}

	public void PlaceMole(GameObject type)
	{
		if (mole)
			return;

        if (!type)
            return;

		CreateHole();

		hole.GetComponent<Hole>().DisplayParticles();
		mole = (GameObject)Instantiate(type, transform.position, moleRotation);
		mole.transform.parent = gameObject.transform;
		mole.GetComponent<Mole>().UpdateGridPosition(posX, posY);

        if (mole.GetComponent<Explosion>())
        {
            foreach (Spawner s in Grid.GetSpawnerRadius(posX, posY, 1))
            {
                s.NearbyExplosions++;
            }

        }

        if (mole.GetComponent<Elektro>())
        {
            List<Spawner> sList = Grid.GetSpawnerLine(posX, posY, true);
            sList.Remove(this);
            sList.AddRange(Grid.GetSpawnerLine(posX, posY, false));
            
            foreach (Spawner s in sList)
                s.NearbyElektros++;
        }

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
			if (obj != null)
                if (obj != this)
                    if (obj.mole == null)
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
			if (obj != null)
			    if (obj != this)
			        if (obj.mole == null)
			            m.Add (obj);

		}
		return m;
	}

    private List<Spawner> GetListWithinExplosion()
    {
        List<Spawner> m = new List<Spawner>();

        for (int i = posX - 1; i <= posX + 1; i++)
        {
            for (int j = posY - 1; j <= posY  + 1; j++)
            {
                Spawner s = Grid.GetSpawner(i, j);
                
                if (s != null)
                    m.Add(s);
            }
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
				s.PlaceJelly();
			}
		}
		if (mole.GetComponent<Explosion> ())
		{
			Spawner s = GetRandomWithinExplosion();
			if (s)
			{
				s.PlaceJelly();
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
