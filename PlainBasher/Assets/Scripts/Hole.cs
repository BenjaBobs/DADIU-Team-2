using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {

    public GameObject particlesPrefab;
    public float particlesYOffset = 0.0f;

	// Use this for initialization
	void Start () {
        DisplayParticles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayParticles()
    {
        if (particlesPrefab.gameObject)
        {
            Vector3 particlesPosition = transform.position;
            particlesPosition.y += particlesYOffset;
            GameObject particles = (GameObject)Instantiate(particlesPrefab, particlesPosition, transform.rotation);
            particles.transform.parent = transform;
        }
    }
}
