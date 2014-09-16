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
        foreach (SingleBlobPlacementProperties pp in manualBlobs)
        {
            StartCoroutine(PlaceSingleBlob(pp));
        }
    }
    IEnumerator PlaceSingleBlob(SingleBlobPlacementProperties props)
    {
        yield return new WaitForSeconds(props.waitTime);

    }
}
