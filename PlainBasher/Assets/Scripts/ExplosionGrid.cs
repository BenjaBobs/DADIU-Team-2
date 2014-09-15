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

	void Start () {
	
	}

	void Update () {
		Vector3 scale = transform.localScale;
		scale.x += Settings.instance.GetDeltaTime() * expandSpeed / lifetime;
		scale.z += Settings.instance.GetDeltaTime() * expandSpeed / lifetime;
		transform.localScale = scale;

		HitWithinRange ();

		currentLifetime += Settings.instance.GetDeltaTime();

		if (currentLifetime >= lifetime) {
			Destroy(gameObject);		
		}
	}

	void HitWithinRange()
	{
		for (int x = posX-radius; x <= posX+radius; x++)
		{
			for (int y = posY-radius; y <= posY+radius; y++)
			{
				GameObject obj = Grid.LookupGrid(x,y);
				if (obj)
				{
					Mole mole = obj.GetComponent<Mole>();
					if (true)
					{
                        mole.OnDeath();
					}
				}
			}
		}
	}
}
