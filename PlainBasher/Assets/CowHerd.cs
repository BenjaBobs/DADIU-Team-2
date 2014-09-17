using UnityEngine;
using System.Collections;

public class CowHerd : MonoBehaviour {

	public GameObject cow1;
	public GameObject cow2;
	public GameObject cow3;
    public GameObject StealingJelly;
	private int cow;
	public static CowHerd instance;
	private

	// Singleton
	void Awake()
	{
		instance = this;

		cow1.AddComponent<TapCow>().cownum = 1;
		cow2.AddComponent<TapCow>().cownum = 2;
		cow3.AddComponent<TapCow>().cownum = 3;
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
            Vector3 pos = cow1.transform.position;
            pos.y -= 2;
            pos.x += 1;
            pos.z += 1;
            GameObject JLY = Instantiate(StealingJelly, pos, Quaternion.Euler(0,180,0)) as GameObject;
            JLY.GetComponentInChildren<Animator>().SetTrigger("MunchMunch");
            cow1.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow1.SetActive(false);
			cow1.GetComponent<BoxCollider>().enabled = false;
			AudioManager.PlayLoseLife();
		}
		if (cow == 2)
		{
            Vector3 pos = cow2.transform.position;
            pos.y -= 2;
            pos.x += 1;
            pos.z += 1;
            GameObject JLY = Instantiate(StealingJelly, pos, Quaternion.Euler(0, 180, 0)) as GameObject;
            JLY.GetComponentInChildren<Animator>().SetTrigger("MunchMunch");
            cow2.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow2.SetActive(false);
			cow2.GetComponent<BoxCollider>().enabled = false;
			AudioManager.PlayLoseLife();
		}
		if (cow == 3)
		{
            Vector3 pos = cow3.transform.position;
            pos.y -= 2;
            pos.x += 1;
            pos.z += 1;
            GameObject JLY = Instantiate(StealingJelly, pos, Quaternion.Euler(0, 180, 0)) as GameObject;
            JLY.GetComponentInChildren<Animator>().SetTrigger("MunchMunch");
            cow3.GetComponentInChildren<Animator>().SetTrigger("Taken");
			//cow3.SetActive(false);
			cow3.GetComponent<BoxCollider>().enabled = false;
			AudioManager.PlayLoseLife();
		}

		//Destroy(animation);
	}

	/*
	protected override void PlayDeathSound()
	{

	}
	*/
}
