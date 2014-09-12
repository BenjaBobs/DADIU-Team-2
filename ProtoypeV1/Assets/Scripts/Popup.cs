using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	GameObject player;
	public float popDistance = 5.0f;
	public float popSpeed = 0.1f;
	public float currentDistance = 0.0f;
	public float timeUp = 3.0f;
	float currentTimeUp = 0.0f;
	bool movingDown = false; 
	public float lerpSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
        
		float currentPopSpeed = (currentDistance / popDistance);
		if (!movingDown)
						currentPopSpeed = 1 - currentPopSpeed;
		float distance = (currentPopSpeed * lerpSpeed + popSpeed) * Time.deltaTime * GetDifficultyMultiplier();
		if (!movingDown) {
			distance = Mathf.Min (distance + currentDistance, popDistance) - currentDistance;
		}
		Vector3 currentPosition = transform.position;

		if (movingDown && currentTimeUp < timeUp) {
			distance = 0;
			currentTimeUp += Time.deltaTime * GetDifficultyMultiplier();
		}

		if (movingDown) {
			distance *= -1;
		}
		currentPosition.y += distance;
		transform.position = currentPosition;

		currentDistance += distance;

		if (currentDistance >= popDistance) {
			movingDown = true;		
		} 
		else if (currentDistance < 0) 
		{
			HitPlayer ();
			Destroy (gameObject);
		}
        
	}

	void HitPlayer()
	{
		((Player)player.gameObject.GetComponent (typeof(Player))).LoseHealth ();
	}

	float GetDifficultyMultiplier()
	{
		return ((Player)player.gameObject.GetComponent (typeof(Player))).GetDifficultySpeed();
	}
}
