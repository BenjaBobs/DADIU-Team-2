using UnityEngine;
using System.Collections;

public class ExplosionLine : MonoBehaviour {
	public float lifetime;
	public float expandSpeed;
	float currentLifetime = 0.0f;
	[HideInInspector]
	public bool expandY;
	float currentWidth = 0;
	public float raycastDistanceFactor;
	public int posX;
	public int posY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		if (expandY) 
		{
			scale.z += Time.deltaTime * expandSpeed / lifetime;
			currentWidth = scale.z * raycastDistanceFactor;
			CheckExplosion(new Vector3(0,0,1));
			CheckExplosion(new Vector3(0,0,-1));
		}
		else
		{
			scale.x += Time.deltaTime * expandSpeed / lifetime;
			currentWidth = scale.x * raycastDistanceFactor;
			CheckExplosion(new Vector3(1,0,0));
			CheckExplosion(new Vector3(-1,0,0));
		}
		transform.localScale = scale;
		currentLifetime += Time.deltaTime;

		if (currentLifetime >= lifetime) {
			Destroy(gameObject);		
		}
	}

	void CheckExplosion(Vector3 direction)
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit))
		{
			if (hit.distance <= currentWidth)	
			{
				Mole hitMole = hit.transform.gameObject.GetComponent<Mole>();
				if (hitMole)
					hitMole.Die(hitMole.allowChainReaction);
			}
		}
	}
}
