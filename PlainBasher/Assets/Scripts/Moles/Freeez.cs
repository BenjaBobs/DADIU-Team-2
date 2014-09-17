using UnityEngine;
using System.Collections;

public class Freeez : Mole {

    static GameObject iceLayerPrefab;
    static bool hasLoaded = false;
    public GameObject iceChargingPrefab;
    public float iceChargingOffset = 0.5f;
    public GameObject iceSteamPrefab;
    public float iceSteamOffset = 0.0f;
    GameObject iceCharging;
    GameObject iceSteam;

    void Start()
    {
        if (!hasLoaded)
        {
            iceLayerPrefab = Resources.Load<GameObject>("Prefabs/iceBlock");
            hasLoaded = true;
        }
    }

    void SpawnIceBlock()
    {
        Instantiate(iceLayerPrefab, new Vector3(-2.2f, 6.7f, -9.8f), Quaternion.identity);
    }

	public override void OnDeath(bool give_bonus = true)
    {
		AudioManager.StopHoldFreeze(false);
		if (give_bonus) SpawnIceBlock();
		base.OnDeath(give_bonus);
    }

	protected override void OnHold()
	{
		//mist liv
		base.OnDeath();
	}
	
	protected override void OnTap()
	{
		AudioManager.PlayHoldFreeze();
        Vector3 iceChargingPosition = transform.position;
        iceChargingPosition.y += iceChargingOffset;
        iceCharging = (GameObject)Instantiate(iceChargingPrefab, iceChargingPosition, transform.rotation);
        iceCharging.transform.parent = transform;
	}

	protected override void OnRelease() {
		AudioManager.StopHoldFreeze();
        Destroy(iceCharging);
	}
	
	protected override void PlayDeathSound()
	{
		AudioManager.StopHoldFreeze(false);
		AudioManager.PlayDestroyFreeze();
	}

    protected override void OnFlee()
    {
        SpawnIceBlock();
        base.OnFlee();
    }

}