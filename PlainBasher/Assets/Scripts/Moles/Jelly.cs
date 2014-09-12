using UnityEngine;
using System.Collections;

public class Jelly : Mole {

	
	void Start () 
	{
		Health = Random.Range(1, 4);
	}

	protected override void OnTap()
	{
		//mist liv
		Health--;

	}

	private void UpdateScale()
	{
		transform.localScale = new Vector3(1,1,1)*Health;
	}

	public override void OnDeath()
	{
		//forsvind/eksplodér/bliv skåret over etc
		base.OnDeath();
	}

	protected override void OnHealthChange()
	{
		UpdateScale ();
	}
}
