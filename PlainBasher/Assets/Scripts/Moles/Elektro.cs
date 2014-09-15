using UnityEngine;
using System.Collections;

public class Elektro : Mole
{

    public override void OnDeath()
    {
        GameObject exploder = Resources.Load<GameObject>("Prefabs/ExplosionLine");

        ExplosionLine line = null;
        GameObject explosionX = Instantiate(exploder, transform.position, transform.rotation) as GameObject;
        line = explosionX.GetComponent<ExplosionLine>();
        line.expandY = false;
        line.posX = posX;
        line.posY = posY;
        GameObject explosionY = Instantiate(exploder, transform.position, transform.rotation) as GameObject;
        line = explosionY.GetComponent<ExplosionLine>();
        line.expandY = true;
        line.posX = posX;
        line.posY = posY;

        
        base.OnDeath();
    }

	protected override void OnTap()
	{
		//mist liv
		Health--;
	}

	protected override void PlayDeathSound()
	{
		AudioManager.PlayDestroyElektro ();
	}
}
