using UnityEngine;
using System.Collections;

public class Elektro : Mole
{

    protected override void OnHold()
    {
        GameObject exploderX = Resources.Load<GameObject>("Prefabs/ExplosionLine");
        GameObject exploderY = Resources.Load<GameObject>("Prefabs/ExplosionLine");

        ExplosionLine line = null;
        GameObject explosionX = Instantiate(exploderX, transform.position, transform.rotation) as GameObject;
        line = explosionX.GetComponent<ExplosionLine>();
        line.expandY = false;
        line.posX = posX;
        line.posY = posY;
        GameObject explosionY = Instantiate(exploderY, transform.position, transform.rotation) as GameObject;
        line = explosionY.GetComponent<ExplosionLine>();
        line.expandY = true;
        line.posX = posX;
        line.posY = posY;


        base.OnDeath();
    }
}
