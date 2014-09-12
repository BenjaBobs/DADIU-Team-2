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
		UpdateHealthHUD ();
		UpdateScoreHUD ();
	}
	
	// Update is called once per frame
	void Update () {
		difficultyMultiplier += Time.deltaTime * difficultyTimeMultiplier;
	}

	public void IncreaseScore(int score)
	{
		currentScore += score;
		UpdateScoreHUD ();
	}

	public void LoseHealth()
	{
		health--;
		UpdateHealthHUD ();
	}

	private void UpdateScoreHUD()
	{
		scoreText = GameObject.Find ("ScoreText");
		GUIText guiText = ((GUIText)scoreText.GetComponent (typeof(GUIText)));
		guiText.text = Localization.Instance.GetString(Localization.LocKey.Score)+": " + currentScore;
	}

	private void UpdateHealthHUD()
	{
		scoreText = GameObject.Find ("HealthText");
		GUIText guiText = ((GUIText)scoreText.GetComponent (typeof(GUIText)));
		guiText.text = Localization.Instance.GetString(Localization.LocKey.Health) + ": " + health;
	}
}