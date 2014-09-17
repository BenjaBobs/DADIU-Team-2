using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManualSpawner : MonoBehaviour {


    //blobs that can be placed manually in a given point in time;
    public List<SingleBlobPlacementProperties> manualBlobs = new List<SingleBlobPlacementProperties>();
    [System.Serializable]
    public class SingleBlobPlacementProperties
    {
        public int positionX;
        public int positionY;
        public float waitTime;
        //public int hitNumber;
        public enum BlobTypes { Jelly1, Jelly2, Jelly3, Elektro, Explosion, Freeez };
        public BlobTypes type = BlobTypes.Jelly1;
        [HideInInspector]
        public GameObject blobType;
    }

    public List<int> eventTimers;
    float waitForSeconds = 1f;

    private int timeToNextEvent;


    private static ManualSpawner _staticRef;

    List<Spawner> allSpawners = new List<Spawner>();


    public static ManualSpawner staticRef
    {
        get
        {
            if (!_staticRef)
            {
                ManualSpawner[] scripts = FindObjectsOfType(typeof(ManualSpawner)) as ManualSpawner[];
                foreach (ManualSpawner ms in scripts)
                {
                    _staticRef = ms;
                    break;
                }
            }
            return _staticRef;
        }
    }

	void Start () {
 
	}


	void Update () {

	}

    void FindSpawners()
    {
        //place all spawner scripts in list
        for (int x = 1; x <= Grid.GetMaxX(); x++)
        {
            for (int y = 1; y <= Grid.GetMaxY(); y++)
            {
                Spawner s = Grid.GetSpawner(x, y);
                if(s) allSpawners.Add(s);
            }
        }

        SetBlobType();

        if (eventTimers.Count != 0)
            timeToNextEvent = eventTimers[0];
        else
            return;
    }

    void SetBlobType()
    {
        List<GameObject> prefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Moles"));

        foreach(SingleBlobPlacementProperties pp in manualBlobs)
        {
            switch (pp.type)
            {
                case SingleBlobPlacementProperties.BlobTypes.Jelly1:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Jelly");
                    break;
                case SingleBlobPlacementProperties.BlobTypes.Jelly2:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Jelly");
                    break;
                case SingleBlobPlacementProperties.BlobTypes.Jelly3:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Jelly");
                    break;
                case SingleBlobPlacementProperties.BlobTypes.Elektro:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Elektro");
                    break;
                case SingleBlobPlacementProperties.BlobTypes.Explosion:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Explosion");
                    break;
                case SingleBlobPlacementProperties.BlobTypes.Freeez:
                    pp.blobType = prefabs.Find(x => x.gameObject.name == "Freeez");
                    break;
            }
        }
    }


    //Methods for manual blob placement
    public void ManuallyPlaceBlobs(bool isForGameOverScreen)
    {
        //Call this from other script
        StartCoroutine(BlobPlacement(isForGameOverScreen));
    }


    void StopSpawning()
    {
        //Stop further spawning
        foreach(Spawner s in allSpawners)
        {
            s.isSpawning = false;
        }

        //Removal of existing blobs
        Grid.WipeBoard();
    }


    //Start standard spawn again
    void StartSpawning()
    {
        foreach (Spawner s in allSpawners)
            s.isSpawning = true;
    }


    //Placement of event blobs. Stops all other spawning while event is running
    IEnumerator BlobPlacement(bool forGameOverScreen)
    {

        FindSpawners();


        bool anyMoreEvents = true;
        int listProgress = 0;
        float seconds;

        while (anyMoreEvents)
        {
            seconds = 0;

            //wait for next event
            while(seconds < timeToNextEvent)
            {
                //TODO: tjek, om spillet er paused
                while (Settings.instance.GetPaused() && !forGameOverScreen)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(waitForSeconds);
                seconds = seconds + waitForSeconds;

            }
            
            


            //wipe board and stop the spawning
            StopSpawning();


            //spawn manually
            float wait = 0;

            for (int i = 0; i < manualBlobs.Count; i++)
            {
                while (Settings.instance.GetPaused() && !forGameOverScreen)
                {
                    yield return null;
                }

                if (i > 0)
                {
                    wait = manualBlobs[i].waitTime - manualBlobs[i - 1].waitTime;
                }
                else
                {
                    wait = manualBlobs[i].waitTime;
                }

                //wait for the specific blob to spawn
                yield return new WaitForSeconds(wait);
                //TODO: pause her

                //spawn blob
                Debug.Log("Placing blob number " + i);
                Spawner theSpawner = Grid.GetSpawner(manualBlobs[i].positionX, manualBlobs[i].positionY);
                theSpawner.PlaceMole(manualBlobs[i].blobType);

                if(forGameOverScreen)
                {
                    Jelly blob = theSpawner.gameObject.transform.GetChild(theSpawner.gameObject.transform.childCount-1).GetComponent<Jelly>();
                    blob.timeUp = 1000;
                }
                

                
            }


            //wait for the board to be cleared
            bool anyMolesLeft = true;

            while(anyMolesLeft == true)
            {
                while (Settings.instance.GetPaused() && !forGameOverScreen)
                {
                    yield return null;
                }
                anyMolesLeft = false;
                for (int x = 0; x < Grid.GetMaxX(); x++)
                {
                    for (int y = 0; y < Grid.GetMaxY(); y++)
                    {
                        if(Grid.GetMole(x,y))
                            anyMolesLeft = true;
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }

            //wipes board, just in case
            Grid.WipeBoard();

            //start spawning again
            StartSpawning();


            //if no more events, make the loop exit
            listProgress++;

            if (listProgress == eventTimers.Count)
                anyMoreEvents = false;
            else
                timeToNextEvent = eventTimers[listProgress] - eventTimers[listProgress-1];
        }


    }
}
