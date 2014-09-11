using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	GameObject player;
	public int minHealth = 1;
	public int maxHealth = 1;
	public float healthScaleMultiplier = 1.0f;
	int currentHealth;
	public int score;
	public Transform exploderY;
	public Transform exploderX;

    public GameObject iceLayer;

	public Transform exploderGrid;
	public bool allowChainReaction;
	public int occurenceFactor = 1;
	public int posX;
	public int posY;
	Vector3 originalColliderSize;

	// Use this for initialization
	void Start () {
		originalColliderSize = transform.gameObject.GetComponent<BoxCollider> ().size;
		// Random color
		//gameObject.renderer.material.color = new Color (Random.value, Random.value, Random.value);
		player = GameObject.Find ("Player");
		currentHealth = Random.Range(minHealth, maxHealth + 1);
		UpdateScale ();
		// Get Spawner
		Spawner spawner = transform.parent.GetComponent<Spawner> ();
		posX = spawner.posX;
		posY = spawner.posY;
		// Get GridSpawner
		GridSpawner gridSpawner = spawner.transform.parent.GetComponent<GridSpawner>();
		gridSpawner.InsertToGrid (posX, posY, gameObject);
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		currentHealth--;
		UpdateScale ();
		if (currentHealth < 1) {
			Die(true);
			GridSpawner gridSpawner = transform.parent.parent.GetComponent<GridSpawner>();
			gridSpawner.RemoveFromGrid(posX, posY);
		}
	}

	public void Die(bool effect = false)
	{
		if (gameObject.name.Contains("Exploder1Mole")) 
		{
			ExplosionLine line = null;
			Transform explosionX = (Transform)Instantiate(exploderX, transform.position, transform.rotation);
			line = explosionX.gameObject.GetComponent<ExplosionLine>();
			line.expandY = false;
			line.posX = posX;
			line.posY = posY;;
			Transform explosionY = (Transform)Instantiate(exploderY, transform.position, transform.rotation);
			line = explosionY.gameObject.GetComponent<ExplosionLine>();
			line.expandY = true;
			line.posX = posX;
			line.posY = posY;
		}
		else if (gameObject.name.Contains("Exploder2Mole")) 
		{
			Transform exTransform = (Transform)Instantiate(exploderGrid, transform.position, transform.rotation);
			ExplosionGrid explosion = exTransform.gameObject.GetComponent<ExplosionGrid>();
			explosion.posX = posX;
			explosion.posY = posY;
		}
        else if (gameObject.name.Contains("FreezeMole"))
        {
            GameObject ice = Instantiate(iceLayer, new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
        }
		((Player)player.gameObject.GetComponent(typeof(Player))).IncreaseScore(score);
		Destroy (gameObject);
	}

	void UpdateScale()
	{
		float scale =  (1 + (currentHealth - 1) * healthScaleMultiplier);
		transform.localScale = new Vector3(1,1,1) * scale;
		Vector3 colliderSize = originalColliderSize;
		colliderSize /= scale;
		transform.gameObject.GetComponent<BoxCollider> ().size = colliderSize;
	}
}