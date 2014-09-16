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
        RemoveExistingBlobs();
        StartCoroutine(BlobPlacement());
    }


    void RemoveExistingBlobs()
    {
        //Removal of existing blobs
        //Stop further spawning
    }


    IEnumerator BlobPlacement()
    {

        for (int i = 0; i < manualBlobs.Count; i++)
        {
            if (i > 0)
            {
                yield return new WaitForSeconds(manualBlobs[i].waitTime - manualBlobs[i - 1].waitTime);
            }
            else
            {
                yield return new WaitForSeconds(manualBlobs[i].waitTime);
            }

            Grid.GetSpawner(manualBlobs[i].positionX, manualBlobs[i].positionY);
        }


    }
}
