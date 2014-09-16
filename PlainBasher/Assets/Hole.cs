using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {

    public GameObject particlesPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DisplayParticles()
    {
        if (particlesPrefab)
        {
            GameObject particles = (GameObject)Instantiate(particlesPrefab, transform.position, transform.rotation);
            particles.transform.parent = transform;
        }
    }
}
