using UnityEngine;
using System.Collections;

public class guiPlayer : MonoBehaviour {

	private bool sc = false;
	private int[] lhs;
	private float flashtimer;
	public string lives = "Lives";
	public string score = "Score";
	public string highScore = "Your Best";
	public string highScoreOnline = "Best Ever";
	public static guiPlayer staticref;
	public GUISkin menuSkin;
	private int[] localHighcore;
	private int onlineHighscore;

	void Awake() {
		staticref = this;
		localHighcore = ScoreManager.GetLocalHighscore();
		onlineHighscore = ScoreManager.GetTotalHighscore();
	}

	void OnGUI () {
		GUI.skin = menuSkin;
		GUI.color = new Color (184f/255f, 82f/255f, 156f/255f);
		GUI.skin.label.fontSize = 35;

		int y = 5;

		if (onlineHighscore > 0) {
			GUI.Label(new Rect(15, y, 500, 50), Localization.instance.GetString(Localization.LocKey.OnlineBest) + ": " + onlineHighscore);
			y += 40;
		}
		else
			onlineHighscore = ScoreManager.GetTotalHighscore();

		GUI.Label(new Rect(15, y, 500, 50), Localization.instance.GetString(Localization.LocKey.LocalBest) + ": " + localHighcore[0]);
		y += 40;
		GUI.Label(new Rect(15, y, 500, 50), Localization.instance.GetString(Localization.LocKey.Score) + ": " + Player.Score);
		//y += 30;
		//GUI.Label(new Rect(10, y, 500, 50), Localization.instance.GetString(Localization.LocKey.Cows) + ": " + Player.Lives);

		/*
		guiScores.text = lives + Player.Lives + "\n"
				//+ score + ": " + (int)Mathf.Lerp (0f, (float)Player.Score, Time.time*0.1f) + "\n"
				+ score + ": " + Player.Score + "\n"
				+ highScore + ": " + lhs[0] + "\n"
				+ highScoreOnline + ": " + ScoreManager.GetTotalHighscore();
		*/
	}
	
	
}
