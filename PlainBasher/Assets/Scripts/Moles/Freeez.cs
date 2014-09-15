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

	public override void OnDeath()
    {
        GameObject ice = Instantiate(iceLayerPrefab, new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
        base.OnDeath();
    }

	protected override void OnHold()
	{
		//mist liv
        base.OnDeath();
	}

	protected override void PlayDeathSound()
	{
		AudioManager.PlayDestroyFreeze ();
	}
}
