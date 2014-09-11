using UnityEngine;
using System.Collections;

public class ExplosionGrid : MonoBehaviour {
	public float lifetime;
	public float expandSpeed;
	float currentLifetime = 0.0f;
	float currentWidth = 0;
	public float raycastDistanceFactor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;

		scale.x += Time.deltaTime * expandSpeed / lifetime;
		scale.z += Time.deltaTime * expandSpeed / lifetime;
		currentWidth = scale.z * raycastDistanceFactor;
		// Orthogonals
		CheckExplosion(new Vector3(0,0,1));
		CheckExplosion(new Vector3(0,0,-1));
		CheckExplosion(new Vector3(-1,0,0));
		CheckExplosion(new Vector3(1,0,0));
		// Diagonals
		CheckExplosion(new Vector3(1,0,1));
		CheckExplosion(new Vector3(1,0,-1));
		CheckExplosion(new Vector3(-1,0,1));
		CheckExplosion(new Vector3(-1,0,-1));

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
