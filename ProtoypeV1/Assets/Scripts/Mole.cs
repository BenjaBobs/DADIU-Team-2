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
	public bool allowChainReaction;

	// Use this for initialization
	void Start () {
		// Random color
		//gameObject.renderer.material.color = new Color (Random.value, Random.value, Random.value);
		player = GameObject.Find ("Player");
		currentHealth = Random.Range(minHealth, maxHealth + 1);
		transform.localScale = transform.localScale * (1 + (currentHealth - 1) * healthScaleMultiplier);
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		currentHealth--;
		if (currentHealth < 1) {
			Die(true);		
		}
	}

	public void Die(bool effect = false)
	{
		if (gameObject.name.Contains("Exploder1Mole")) 
		{
			Transform explosionX = (Transform)Instantiate(exploderX, transform.position, transform.rotation);
			explosionX.gameObject.GetComponent<Explosion>().expandY = false;
			Transform explosionY = (Transform)Instantiate(exploderY, transform.position, transform.rotation);
			explosionY.gameObject.GetComponent<Explosion>().expandY = true;
		}
		((Player)player.gameObject.GetComponent(typeof(Player))).IncreaseScore(score);
		Destroy (gameObject);
	}


}