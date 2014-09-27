using UnityEngine;
using System.Collections;

public class Elektro : Mole
{
    public GameObject lightning;
    public override void OnDeath(bool give_bonus = true)
    {
		if (isDead)
			return;

		// need it here to avoid infinite loop
		isDead = true;

		if (give_bonus)
		{
			LightningSpawner (90);
			/*
			for (int i = 0; i <= 270; i = i + 90) {
					LightningSpawner (i);
			}
			*/
			DestroyNearbyMoles (false);
			DestroyNearbyMoles (true);
		}

		// once again to avoid infinte loops
		isDead = false;
        Grid.GetSpawner(posX, posY).NearbyElektros--;
		base.OnDeath(give_bonus);
    }

    protected override void OnTap()
    {
        //mist liv
        Health--;
    }

    void LightningSpawner(int theRotation)
    {
		Quaternion rot = Quaternion.Euler (0, theRotation, 0);
		Instantiate(lightning, transform.position, rot); 
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
			Mole obj = (expandY ? Grid.GetMole(posX, i) : Grid.GetMole(i, posY));

            Spawner s = (expandY ? Grid.GetSpawner(posX, i) : Grid.GetSpawner(i, posY));
            if (s)
                s.NearbyElektros--;

			if (!obj) continue;
			if (obj.gameObject == gameObject) continue;
			if (obj.IsDead()) continue;

            obj.OnChain();
			obj.OnDeath();
		}
	}
}
