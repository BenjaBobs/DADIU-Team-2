using UnityEngine;
using System.Collections;

public class CowHerd : MonoBehaviour {

	public GameObject deathAnimation;
	public GameObject cow1;
	public GameObject cow2;
	public GameObject cow3;
	private int cow;
	public static CowHerd instance;
	private

	// Singleton
	void Awake()
	{
		instance = this;
	}

	void Update()
	{

	}

	public void KillCow(int cowToKill)
	{
		switch (cowToKill)
		{
			case 1:
				OnDeath(cowToKill);
				break;
			case 2:
				OnDeath(cowToKill);
				break;
			case 3:
				OnDeath(cowToKill);
				break;
			default:
				break;
		}
	}

	void OnDeath(int cow)
	{
		GameObject animation;
		Quaternion rot = Quaternion.identity;

		if (cow == 1)
		{
			cow1.SetActive(false);
			//animation = Instantiate (deathAnimation, cow1.transform.position, rot) as GameObject;
		}
		if (cow == 2)
		{
			cow2.SetActive(false);
			//animation = Instantiate (deathAnimation, cow2.transform.position, rot) as GameObject;
		}
		if (cow == 3)
		{
			cow3.SetActive(false);
			//animation = Instantiate (deathAnimation, cow3.transform.position, rot) as GameObject;
		}

		//Destroy(animation);
	}

	/*
	protected override void PlayDeathSound()
	{

	}
	*/
}
