using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	int currentScore;
	public int health;
	public float difficultyMultiplier = 1.0f;
	public float difficultyTimeMultiplier = 0.1f;
	GameObject scoreText;

	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		difficultyMultiplier += Time.deltaTime * difficultyTimeMultiplier;
	}

	public void IncreaseScore(int score)
	{
		scoreText = GameObject.Find ("ScoreText");
		currentScore += score;
		GUIText guiText = ((GUIText)scoreText.GetComponent (typeof(GUIText)));
		guiText.text = "Score: " + currentScore;
	}
}
