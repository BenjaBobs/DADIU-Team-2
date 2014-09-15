using UnityEngine;
using System.Collections;

public class Explosion : Mole {

	public override void OnDeath()
    {
        GameObject explosionGrid = Resources.Load<GameObject>("Prefabs/ExplosionGrid");

        GameObject exTransform = Instantiate(explosionGrid, transform.position, transform.rotation) as GameObject;
        ExplosionGrid explosion = exTransform.GetComponent<ExplosionGrid>();
        explosion.posX = posX;
        explosion.posY = posY;

        base.OnDeath();
    }

	protected override void OnTap()
	{
		//mist liv
		Health--;
	}
}
