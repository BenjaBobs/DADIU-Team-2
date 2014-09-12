using UnityEngine;
using System.Collections;

public class Explosion : Mole {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnDeath()
    {
        ExplosionGrid explosionGrid = Resources.Load<ExplosionGrid>("ExplosionGrid");

        Transform exTransform = (Transform)Instantiate(explosionGrid, transform.position, transform.rotation);
        ExplosionGrid explosion = exTransform.gameObject.GetComponent<ExplosionGrid>();
        explosion.posX = posX;
        explosion.posY = posY;

        base.OnDeath();
    }
}
