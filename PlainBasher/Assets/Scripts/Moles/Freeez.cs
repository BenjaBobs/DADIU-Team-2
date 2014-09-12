using UnityEngine;
using System.Collections;

public class Freeez : Mole {

    static GameObject iceLayerPrefab;
    static bool hasLoaded = false;

    void Start()
    {
        if (!hasLoaded)
        {
            iceLayerPrefab = Resources.Load<GameObject>("Prefabs/iceLayerPrefab");
            hasLoaded = true;
        }
    }

    protected override void OnHold()
    {
        GameObject ice = Instantiate(iceLayerPrefab, new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
        OnDeath();
    }
}
