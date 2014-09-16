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
		lhs = ScoreManager.GetLocalHighscore();


		GUI.Label(new Rect(10, 40, 100, 20), Localization.instance.GetString(Localization.LocKey.Cows) + ": " + Player.Lives);
        GUI.Label(new Rect(10, 60, 100, 20), Localization.instance.GetString(Localization.LocKey.Score) + ": " + Player.Score);
        GUI.Label(new Rect(10, 80, 200, 20), Localization.instance.GetString(Localization.LocKey.LocalBest) + ": " + lhs[0]);
        GUI.Label(new Rect(10, 100, 200, 20), Localization.instance.GetString(Localization.LocKey.OnlineBest) + ": " + ScoreManager.GetTotalHighscore());

		/*
		guiScores.text = lives + Player.Lives + "\n"
				//+ score + ": " + (int)Mathf.Lerp (0f, (float)Player.Score, Time.time*0.1f) + "\n"
				+ score + ": " + Player.Score + "\n"
				+ highScore + ": " + lhs[0] + "\n"
				+ highScoreOnline + ": " + ScoreManager.GetTotalHighscore();
		*/
	}
	
	
}
