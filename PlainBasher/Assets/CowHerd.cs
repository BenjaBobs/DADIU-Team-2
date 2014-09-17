using UnityEngine;
using System.Collections;

public class CowHerd : MonoBehaviour {

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
		Quaternion rot = Quaternion.identity;

		if (cow == 1)
		{
            cow1.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow1.SetActive(false);
		}
		if (cow == 2)
		{
            cow2.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow2.SetActive(false);
		}
		if (cow == 3)
		{
            cow3.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow3.SetActive(false);
		}

		//Destroy(animation);
	}

	/*
	protected override void PlayDeathSound()
	{

	}
	*/
}
