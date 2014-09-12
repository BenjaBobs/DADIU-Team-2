using UnityEngine;
using System.Collections;

public class Elektro : Mole
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnDeath()
    {
        ExplosionLine exploderX = Resources.Load<ExplosionLine>("ExplosionLine");
        ExplosionLine exploderY = Resources.Load<ExplosionLine>("ExplosionLine");

        ExplosionLine line = null;
        Transform explosionX = (Transform)Instantiate(exploderX, transform.position, transform.rotation);
        line = explosionX.gameObject.GetComponent<ExplosionLine>();
        line.expandY = false;
        line.posX = posX;
        line.posY = posY;
        Transform explosionY = (Transform)Instantiate(exploderY, transform.position, transform.rotation);
        line = explosionY.gameObject.GetComponent<ExplosionLine>();
        line.expandY = true;
        line.posX = posX;
        line.posY = posY;


        base.OnDeath();
    }
}
