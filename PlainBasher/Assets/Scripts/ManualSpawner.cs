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
        public int waitTime;
        public GameObject blobType;
        public int hitNumber;
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Methods for manual blob placement
    void ManuallyPlaceBlobs()
    {
        StopSpawning();
        StartCoroutine(BlobPlacement());
    }


    void StopSpawning()
    {
        //Removal of existing blobs
        //Stop further spawning
    }

    void StartSpawning()
    {
        //Start standard spawn again
        //done when every manually placed blob is dealt with (dead or escaped)
    }

    IEnumerator BlobPlacement()
    {
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

            yield return new WaitForSeconds(wait);

            Spawner theSpawner = Grid.GetSpawner(manualBlobs[i].positionX, manualBlobs[i].positionY);

            List<GameObject> moleTypes = new List<GameObject>(Resources.LoadAll<GameObject>("Resources/Moles")); //Rigtig sti?
        }


    }
}
