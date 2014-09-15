using UnityEngine;
using System.Collections;

public class Explosion : Mole {

    public GameObject explosion;

	public override void OnDeath()
    {
		if (isDead)
				return;

        GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        base.OnDeath();
    }

	protected override void OnTap()
	{
		//mist liv
		Health--;
	}


	protected override void PlayDeathSound()
	{
		AudioManager.PlayDestroyExplosion ();
	}

    void DestroyAreaOfMoles()
    {

    }
}





//GameObject explosionGrid = Resources.Load<GameObject>("Prefabs/ExplosionGrid");

//GameObject exTransform = Instantiate(explosionGrid, transform.position, transform.rotation) as GameObject;
//ExplosionGrid explosion = exTransform.GetComponent<ExplosionGrid>();
//explosion.posX = posX;
//explosion.posY = posY;