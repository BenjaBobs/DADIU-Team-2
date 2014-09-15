using UnityEngine;
using System.Collections;

public class Jelly : Mole {

	private Vector3 BoxOriginalSize;
	
	void Start () 
	{
		BoxOriginalSize = gameObject.GetComponent<BoxCollider> ().size;
		Health = Random.Range(1, 4);
	}

	protected override void OnTap()
	{
		//mist liv
		Health--;

	}

	private void UpdateScale()
	{
		float s;
		s = (0.5f + Health * 0.5f);
		transform.localScale = new Vector3(2,2,2) * s;
		gameObject.GetComponent<BoxCollider> ().size = BoxOriginalSize / s;
	}



	protected override void OnHealthChange()
	{
		UpdateScale ();
	}
}
