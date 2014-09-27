using UnityEngine;
using System.Collections;

public class Explosion : Mole
{

    public GameObject explosion;

	public override void OnDeath(bool give_bonus = true)
    {
        if (isDead)
            return;

        isDead = true;

		if (give_bonus)
		{
			Quaternion rot = Quaternion.Euler (270, 0, 0);
			Instantiate (explosion, transform.position, rot);

			DestroyAreaOfMoles ();
		}

        isDead = false;

        for (int i = posX - 1; i <= posX + 1; i++)
        {
            for (int j = posY - 1; j <= posY + 1; j++)
            {
                Spawner s = Grid.GetSpawner(i, j);
                if (s)
                    s.NearbyExplosions--;
            }
        }

		base.OnDeath(give_bonus);
    }

    protected override void OnTap()
    {
        //mist liv
        Health--;
    }

    protected override void PlayDeathSound()
    {
        AudioManager.PlayDestroyExplosion();
    }

    void DestroyAreaOfMoles()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Mole obj = Grid.GetMole(posX + i, posY + j);

                if (obj && !obj.IsDead())
                {
                    obj.OnChain();
                    obj.OnDeath();
                }

            }
        }
    }
}





//GameObject explosionGrid = Resources.Load<GameObject>("Prefabs/ExplosionGrid");

//GameObject exTransform = Instantiate(explosionGrid, transform.position, transform.rotation) as GameObject;
//ExplosionGrid explosion = exTransform.GetComponent<ExplosionGrid>();
//explosion.posX = posX;
//explosion.posY = posY;