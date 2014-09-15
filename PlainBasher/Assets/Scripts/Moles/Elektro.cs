using UnityEngine;
using System.Collections;

public class Elektro : Mole
{
    public GameObject lightning;
    public override void OnDeath()
    {

        for (int i = 0; i <= 270; i = i + 90)
        {
            LightningSpawner(i);
        }


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

        Vector3 positioning;

        switch (theRotation)
        {
            case 0:
                positioning = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.5f);
                break;
            case 90:
                positioning = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y, transform.localPosition.z);
                break;
            case 180:
                positioning = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.5f);
                break;
            case 270:
                positioning = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y, transform.localPosition.z);
                break;
            default:
                positioning = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.5f);
                break;
        }

        GameObject lightningObj = Instantiate(lightning, transform.position, rot) as GameObject;
        
    }
}
