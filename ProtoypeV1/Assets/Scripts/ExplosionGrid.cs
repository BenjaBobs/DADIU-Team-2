using UnityEngine;
using System.Collections;

public class ExplosionGrid : MonoBehaviour {
	public float lifetime;
	public float expandSpeed;
	float currentLifetime = 0.0f;
	float currentWidth = 0;
	public int radius;
	public int posX;
	public int posY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		scale.x += Time.deltaTime * expandSpeed / lifetime;
		scale.z += Time.deltaTime * expandSpeed / lifetime;
		transform.localScale = scale;

		GridSpawner gridSpawner = GameObject.Find ("GridSpawner").GetComponent<GridSpawner>();
		HitWithinRange (gridSpawner);

		currentLifetime += Time.deltaTime;

		if (currentLifetime >= lifetime) {
			Destroy(gameObject);		
		}
	}

	void HitWithinRange(GridSpawner gridSpawner)
	{
		for (int x = posX-radius; x <= posX+radius; x++)
		{
			for (int y = posY-radius; y <= posY+radius; y++)
			{
				GameObject obj = gridSpawner.LookupGrid(x,y);
				if (obj)
				{
					Mole mole = obj.GetComponent<Mole>();
					if (true)
					{
						mole.Die();
					}
				}
			}
		}
	}

	/*OUTDATED:
	 * void CheckExplosion(Vector3 direction)
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
	}*/
}
