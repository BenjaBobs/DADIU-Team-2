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
        public GameObject blobType;
        public int hitNumber;
    }

    public List<int> eventTimers;
    private int timeToNextEvent;

    List<Spawner> allSpawners = new List<Spawner>();



	void Start () {
        //place all spawner scripts in list
        for(int x = 0; x < Grid.GetMaxX(); x++)
        {
            for (int y = 0; y < Grid.GetMaxY(); y++)
            {
                Spawner s = Grid.GetSpawner(x, y);
                allSpawners.Add(s);
            }
        }


        if (eventTimers.Count != 0)
            timeToNextEvent = eventTimers[0];
        else
            return;
	}


	void Update () {

	}

    //Methods for manual blob placement
    public void ManuallyPlaceBlobs()
    {
        //Call this from other script
        StartCoroutine(BlobPlacement());
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
    IEnumerator BlobPlacement()
    {
        bool anyMoreEvents = true;
        int listProgress = 0;

        while (anyMoreEvents)
        {
            //wait for next event
            yield return new WaitForSeconds(timeToNextEvent);


            //wipe board and stop the spawning
            StopSpawning();


            //spawn manually
            float wait = 0;

            for (int i = 0; i < manualBlobs.Count; i++)
            {
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

                Spawner theSpawner = Grid.GetSpawner(manualBlobs[i].positionX, manualBlobs[i].positionY);
                theSpawner.PlaceMole(manualBlobs[i].blobType);
            }


            //wait for the board to be cleared
            bool anyMolesLeft = true;

            while(anyMolesLeft == true)
            {
                anyMolesLeft = false;
                for (int x = 0; x < Grid.GetMaxX(); x++)
                {
                    for (int y = 0; y < Grid.GetMaxY(); y++)
                    {
                        if(Grid.GetMole(x,y))
                            anyMolesLeft = true;
                    }
                }
                yield return new WaitForSeconds(1);
            }

            //wipes board, just in case
            Grid.WipeBoard();


            //if no more events, make the loop exit
            listProgress++;

            if (listProgress == eventTimers.Count)
                anyMoreEvents = false;
            else
                timeToNextEvent = eventTimers[listProgress] - eventTimers[listProgress-1];
        }


    }
}
