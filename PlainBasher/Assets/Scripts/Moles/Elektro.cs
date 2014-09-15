using UnityEngine;
using System.Collections;

public class Elektro : Mole
{
    public GameObject lightning;
    public override void OnDeath()
    {
		if (isDead)
			return;

		// need it here to avoid infinite loop
		isDead = true;

        for (int i = 0; i <= 270; i = i + 90)
        {
            LightningSpawner(i);
        }
		DestroyNearbyMoles (false);
		DestroyNearbyMoles(true);


        /*
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
        */

		// once again to avoid infinte loops
		isDead = false;




        base.OnDeath();
    }

    protected override void OnTap()
    {
        //mist liv
        Health--;
    }

    void LightningSpawner(int theRotation)
    {
        Quaternion rot = Quaternion.Euler(0, theRotation, 0);

        GameObject lightningObj = Instantiate(lightning, transform.position, rot) as GameObject;
        
    }

	protected override void PlayDeathSound()
	{
		AudioManager.PlayDestroyElektro ();
	}

	private void DestroyNearbyMoles(bool expandY)
	{
		int maxValue = (expandY ? Grid.GetMaxY() : Grid.GetMaxX());
		for (int i = 1; i <= maxValue; i++)
		{
			GameObject obj = (expandY ? Grid.LookupGrid(posX, i) : Grid.LookupGrid(i, posY));
			if (obj && obj != gameObject)
			{
				Mole mole = obj.GetComponent<Mole>();
				if (!mole.IsDead ())
				{
					mole.OnDeath();
				}
			}
		}
	}
}
