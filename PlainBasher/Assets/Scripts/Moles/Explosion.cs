using UnityEngine;
using System.Collections;

public class Explosion : Mole {

    protected override void OnHold()
    {
        GameObject explosionGrid = Resources.Load<GameObject>("Prefabs/ExplosionGrid");

        GameObject exTransform = Instantiate(explosionGrid, transform.position, transform.rotation) as GameObject;
        ExplosionGrid explosion = exTransform.GetComponent<ExplosionGrid>();
        explosion.posX = posX;
        explosion.posY = posY;

        base.OnDeath();
    }
}
