using UnityEngine;
using System.Collections;

public class Explosion : Mole
{

    public GameObject explosion;

    public override void OnDeath()
    {
        if (isDead)
            return;

        isDead = true;

        Quaternion rot = Quaternion.Euler(270, 0, 0);


        GameObject explosionObj = Instantiate(explosion, transform.position, rot) as GameObject;

        DestroyAreaOfMoles();

        isDead = false;
        base.OnDeath();
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