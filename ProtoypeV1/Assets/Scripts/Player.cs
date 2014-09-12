using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	int currentScore;
	public int health;
	public List<string> Difficulties = new List<string>();
	public bool FadeBetweenDifficulties = true;
	private Dictionary<float, float> ParsedDifficulties;
	GameObject scoreText;

	// Use this for initialization
	void Start () {
		ParseDifficultyList ();
		currentScore = 0;
		UpdateHealthHUD ();
		UpdateScoreHUD ();
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

	public float GetDifficulty()
	{
		return GetDifficultyAtPosition (Time.timeSinceLevelLoad);
	}
	public float GetDifficultyAtPosition(float p)
	{
		// not parsed
		if (ParsedDifficulties == null)
			return 1.0f;
		// no entries
		if (ParsedDifficulties.Count == 0)
			return 1.0f;

		float lowerBound = -1;
		float upperBound = -1;
		foreach(KeyValuePair<float, float> kvp in ParsedDifficulties)
		{
			float time = kvp.Key;

			// this is prior to this current time
			if (time <= p)
			{
				// we already have a better lower bound
				if (lowerBound > time) continue;
				lowerBound = time;
			}
			// this is the target difficulty
			else
			{
				if (upperBound >= 0 && upperBound < time) continue;
				upperBound = time;
			}
		}

		float lowerBoundValue = lowerBound >= 0 ? ParsedDifficulties [lowerBound] : 1.0f;

		// if no upper bound, we are beyond the last point. Use lower bound.
		if (upperBound < 0)
			return lowerBoundValue;

		float upperBoundValue = ParsedDifficulties [upperBound];

		if (!FadeBetweenDifficulties)
			return lowerBoundValue;

		float alpha = (p - lowerBound) / (upperBound - lowerBound);
		return Mathf.Lerp (lowerBoundValue, upperBoundValue, alpha);
	}

	private void ParseDifficultyList()
	{
		ParsedDifficulties = new Dictionary<float, float>();
		for (int i = 0; i < Difficulties.Count; i++)
		{
			string[] splitString = Difficulties[i].Split(':');
			string timePosition = splitString[0];
			timePosition = timePosition.Trim();
			string difficulityAtPosition = splitString[1];
			difficulityAtPosition = difficulityAtPosition.Trim();
			ParsedDifficulties[float.Parse (timePosition)] = float.Parse(difficulityAtPosition);
		}
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