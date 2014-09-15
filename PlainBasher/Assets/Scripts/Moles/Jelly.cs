using UnityEngine;
using System.Collections;

public class Jelly : Mole {

	private Vector3 BoxOriginalSize;
	public float ScaleYPositionOffset = 2.0f;
	
	void Start () 
	{
		BoxOriginalSize = gameObject.GetComponent<BoxCollider> ().size;
		Health = Random.Range(1, 4);
	}

	protected override void OnTap()
	{
		//mist liv
		Health--;
		if (Health > 0)
			AudioManager.PlayTapBigJelly ();
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
