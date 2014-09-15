using UnityEngine;
using System.Collections;

public class Jelly : Mole {

	private Vector3 BoxOriginalSize;
	public float ScaleYPositionOffset = 2.0f;
	public int MaxJellyHealth = 4;
	public GameObject jelly;
	
	public override void OnDeath ()
	{
		if(isDead)
			return;
		
		isDead = true;
		SplatSpawner();
		isDead = false;
		
		base.OnDeath ();
	}
	
	void SplatSpawner()
	{
		Vector3 positioning;
		GameObject jellyObj = Instantiate(jelly, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.Euler(270,0,0)) as GameObject;
	}
	
	void Start () 
	{
		BoxOriginalSize = gameObject.GetComponent<BoxCollider> ().size;

		int TargetHealth = Random.Range (1, MaxJellyHealth);
		float FatJellyMulti = Settings.instance.GetDifficultyFatJelliesMultiplier ();

		TargetHealth = (int)Mathf.Lerp (1, TargetHealth, FatJellyMulti);
		TargetHealth = Mathf.Max (TargetHealth, 1);
		TargetHealth = Mathf.Min (TargetHealth, MaxJellyHealth);

		Health = TargetHealth;
	}

	protected override void OnTap()
	{
		if (Health > 2)
			AudioManager.PlayTapBigJelly ();
		else if (Health > 1)
			AudioManager.PlayTapSmallJelly ();

		//mist liv
		Health--;
	}

	private void UpdateScale()
	{
		float s = (0.5f + Health * 0.5f);
		transform.localScale = new Vector3(2,2,2) * s;

		// push up mesh if larger than standard
		Vector3 localpos = new Vector3 (0, (s-1.0f)*ScaleYPositionOffset, 0);
		transform.localPosition = localpos;

		// make sure box is always same size
		gameObject.GetComponent<BoxCollider> ().size = BoxOriginalSize / s;
	}



	protected override void OnHealthChange()
	{
		UpdateScale ();
	}

	protected override void PlayDeathSound()
	{
		AudioManager.PlayDestroyJelly();
	}
}
