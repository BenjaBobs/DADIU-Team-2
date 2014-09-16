using UnityEngine;
using System.Collections;

public class guiPlayer : MonoBehaviour {

	private bool sc = false;
	private int[] lhs;
	private float flashtimer;
	//public GUIText guiScores;
	public string lives = "Lives";
	public string score = "Score";
	public string highScore = "Your Best";
	public string highScoreOnline = "Best Ever";
	public static guiPlayer staticref;

	void Awake()
	{
		staticref = this;
	}
	
	void OnGUI () 
	{
		ScoreManager.LoadHighscore(); // load the HS
		sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
		
		lhs = ScoreManager.GetLocalHighscore();


		GUI.Label(new Rect(10, 10, 100, 20), "Lives: " + Player.Lives);
		GUI.Label(new Rect(10, 30, 100, 20), "Score: " + Player.Score);
		GUI.Label(new Rect(10, 50, 200, 20), "Previous Best: " + lhs[0]);

		/*
		guiScores.text = lives + Player.Lives + "\n"
				//+ score + ": " + (int)Mathf.Lerp (0f, (float)Player.Score, Time.time*0.1f) + "\n"
				+ score + ": " + Player.Score + "\n"
				+ highScore + ": " + lhs[0] + "\n"
				+ highScoreOnline + ": " + ScoreManager.GetTotalHighscore();
		*/
	}
	
	
}
