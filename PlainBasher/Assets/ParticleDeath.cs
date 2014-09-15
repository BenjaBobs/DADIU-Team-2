using UnityEngine;
using System.Collections;

public class ParticleDeath : MonoBehaviour {

    float deathTime = 2;
	
	// Update is called once per frame
	void Update () {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0)
            Destroy(gameObject);
	}
}
