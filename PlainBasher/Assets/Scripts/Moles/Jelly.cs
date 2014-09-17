using UnityEngine;
using System.Collections;

public class Jelly : Mole {

	private Vector3 BoxOriginalSize;
	public Vector3 OriginalScale;
	private Vector3 OriginalLocalPosition;
	public float ScaleYPositionOffset = 2.0f;
	public float BounceAnimation = 0;
	public int MaxJellyHealth = 4;
	public GameObject jelly;
	
	public override void OnDeath (bool give_bonus = true)
	{
		if(isDead)
			return;
		
		if (give_bonus)
			SplatSpawner();
		
		base.OnDeath (give_bonus);
	}
	
	void SplatSpawner()
	{
		Instantiate(jelly, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.Euler(270,0,0));
	}
	
	void Start () 
	{
		BoxOriginalSize = gameObject.GetComponent<BoxCollider> ().size;
		OriginalLocalPosition = transform.localPosition;
		OriginalScale = gameObject.transform.localScale;

		int TargetHealth = Random.Range (1, MaxJellyHealth+1);
		float FatJellyMulti = Settings.instance.GetDifficultyFatJelliesMultiplier ();

		TargetHealth = (int)Mathf.Lerp (1, TargetHealth, FatJellyMulti);
		TargetHealth = Mathf.Max (TargetHealth, 1);
		TargetHealth = Mathf.Min (TargetHealth, MaxJellyHealth);

		Health = TargetHealth;
	}

	void Update () 
	{
		MoleMovement();
		if (BounceAnimation > 0.0f)
			UpdateScale ();
	}

	protected override void OnTap()
	{
		if (Health > 2)
			AudioManager.PlayTapBigJelly ();
		else if (Health > 1)
			AudioManager.PlayTapSmallJelly ();

		//mist liv
		DoBounceAnimation ();
		Health--;
	}

	private float GetScale()
	{
		return (1.0f + (Health-MaxJellyHealth) * 0.2f);
	}

	private void DoBounceAnimation()
	{
		BounceAnimation = 1.0f;
	}

	private void UpdateScale()
	{
		if (BounceAnimation > 0.0f)
			BounceAnimation -= Settings.instance.GetDeltaTime ()*4;

		float s = GetScale ();
		Vector3 NewScale = OriginalScale * s;

		// bouncing animation when clicked - Jonas
		if (BounceAnimation > 0.0f)
		{
			float scale = 0.4f * BounceAnimation;
			NewScale.x *= 1 - Mathf.Cos ((BounceAnimation) * Mathf.PI * 2 * 3) * scale;
			NewScale.z = NewScale.x;
			NewScale.y = 1 + Mathf.Cos ((BounceAnimation) * Mathf.PI * 2 * 3) * scale*0.7f;
		}

		transform.localScale = NewScale;

		// push up mesh if larger than standard
		Vector3 localpos = OriginalLocalPosition + new Vector3 (0, (s-1.0f)*ScaleYPositionOffset, 0);
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
