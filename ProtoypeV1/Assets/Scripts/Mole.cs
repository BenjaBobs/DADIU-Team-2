using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	GameObject player;
	public int maxHealth;
	int currentHealth;
	public int score;
	// Use this for initialization
	void Start () {
		// Random color
		//gameObject.renderer.material.color = new Color (Random.value, Random.value, Random.value);
		player = GameObject.Find ("Player");
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		currentHealth--;
		if (currentHealth < 1) {
			Die();		
		}
	}

	void Die()
	{
		((Player)player.gameObject.GetComponent(typeof(Player))).IncreaseScore(score);
		Destroy (gameObject);
	}
}