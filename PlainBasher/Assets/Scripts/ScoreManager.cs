using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ScoreManager : MonoBehaviour {
	// Database connection
	protected readonly string baseURL = "http://chx.dk/connect/dadiu.php";
	protected readonly string highscoreURL = "http://chx.dk/connect/dadiu.php?highscore=1";
	protected readonly string totalHighscoreURL = "http://chx.dk/connect/dadiu.php?total_highscore=1";
	private static bool uploaded = false;
	private static bool highscoreLoaded = false, highscoreDoneLoading = true;
	private static string[] highscore = new string[0];
	private static int oldHighscoreScore = 0;
	private static int numberBest = 3; // Best scores in highscore list
	private static int numberBetter = 3; // Better scores than current in highscore list
	private static int numberWorst = 3; // Worse scores than current in highscore list
	private static int lastScore = 0; // Last saved score
	private static int totalHighscore = 0; // Total highscore

	// Singleton
	public static ScoreManager instance;

	private void Awake() {
		if (instance == null)
			instance = this;
	}
	
	/// <summary>
	/// Returns the local highscore.
	/// If first time, the array only contains the current score.
	/// Else it returns two values. The first is always the new one, and the second is the old.
	/// Remember to perform a check if the new score is higher than the old one.
	/// </summary>
	/// <returns>The local highscore.</returns>
	public static int[] GetLocalHighscore() {
		int[] result;
		if (oldHighscoreScore > 0)
			result = new int[2];
		else
			result = new int[1];

		result[0] = PlayerPrefs.GetInt("highscore_score");
		if (oldHighscoreScore > 0)
			result[1] = oldHighscoreScore;
		
		return result;
	}
	
	/// <summary>
	/// Needs to be called first to load the highscore
	/// </summary>
	public static void LoadHighscore() {
		if (!highscoreLoaded) {
			highscoreLoaded = true;
			instance.StartCoroutine (instance.GetHighscoreData ());
		}
	}

	/// <summary>
	/// Returns the global highscore.
	/// The length of the array is dynamic.
	/// </summary>
	/// <returns>The highscore in a string array</returns>
	public static string[] GetHighscore() {
		return highscore;
	}

	/// <summary>
	/// Determines if is highscore loaded.
	/// </summary>
	/// <returns><c>true</c> if is highscore loaded; otherwise, <c>false</c>.</returns>
	public static bool IsHighscoreLoaded() {
		return highscoreDoneLoading;
	}

	/// <summary>
	/// Returns the id for the string returned from GetHighscore.
	/// The score at the id is the users score.
	/// </summary>
	/// <returns>The identifier of user score.</returns>
	public static int GetIdOfUserScore() {
		return numberBest + numberBetter - 1;
	}

	/// <summary>
	/// Returns the number of best scores shown
	/// </summary>
	/// <returns>The number of best scores.</returns>
	public static int GetNumberOfBestScores() {
		return numberBest;
	}
	
	/// <summary>
	/// Saves the name and score
	/// 
	/// Add score:
	///  ScoreManager.AddScore ("Navn", 0);
	/// </summary>
	/// <param name="name">Name</param>
	/// <param name="score">Score</param>
	public static void AddScore(string name, int score) {
		if (score > totalHighscore)
			totalHighscore = score;

		instance.StartCoroutine(instance.UploadScore(name, score));
		lastScore = score;

		if (!PlayerPrefs.HasKey ("highscore_score")) {
			PlayerPrefs.SetInt ("highscore_score", score);
		}
		else if (score > PlayerPrefs.GetInt ("highscore_score")) {
			oldHighscoreScore = PlayerPrefs.GetInt ("highscore_score");
			PlayerPrefs.SetInt ("highscore_score", score);
		}
	}


	private string[] SplitString(string str, string splitter) {
		return str.Split(new string[] { splitter }, StringSplitOptions.None);
	}


	private IEnumerator GetHighscoreData() {
		WWWForm form;
		WWW www = null;
		
		int index = 0;
		while (www == null && index < 20) {
			index++;
			
			form = new WWWForm();
			form.AddField("best", numberBest.ToString());
			form.AddField("better", numberBetter.ToString());
			form.AddField("worse", numberWorst.ToString());
			form.AddField("score", lastScore.ToString());

			www = new WWW(highscoreURL, form);
			
			yield return www;
		}

		if (www.text != null) {
			highscore = new string[numberBest+numberBetter+numberWorst+1];
			index = 0;
			using (StringReader reader = new StringReader(www.text)) {
				string line;
				while ((line = reader.ReadLine()) != null)
					highscore[index++] = line;
			}

			foreach (string entry in highscore)
				Debug.Log (entry);

			highscoreLoaded = false;
			highscoreDoneLoading = true;
		}
	}


	private IEnumerator UploadScore(string name, int score) {
		WWWForm form;
		WWW www = null;
		
		int index = 0;
		while ((www == null || www.text != "Success") && index < 20 && !uploaded) {
			index++;
			
			form = new WWWForm();
			form.AddField("name", name);
			form.AddField("score", score.ToString());
			form.AddField("secret", name + "e231" + score.ToString());
			
			www = new WWW(baseURL, form);
			
			yield return www;
		}
		
		if (index < 20) {
			uploaded = true;
			Debug.Log ("uploaded:" + name  + " score:" + score);
		}
		else
			Debug.Log ("not uploaded");
	}


	public static int GetTotalHighscore() {
		return totalHighscore;
	}
	
	public static void LoadTotalHighscore() {
		instance.StartCoroutine(instance.LoadTotalHighscoreDB());
	}
	
	private IEnumerator LoadTotalHighscoreDB() {
		WWW www = null;
		
		int index = 0;
		while (www == null && index < 50) {
			index++;
			
			www = new WWW(totalHighscoreURL);
			
			yield return www;
		}
		
		if (index < 50)
			totalHighscore = int.Parse(www.text);
		else
			totalHighscore = !PlayerPrefs.HasKey ("highscore_score") ? 0 : PlayerPrefs.GetInt ("highscore_score");
	}
	
	
	private byte[] GetBytes(string str) {
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}
	
	private string GetString(byte[] bytes) {
		char[] chars = new char[bytes.Length / sizeof(char)];
		System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
		return new string(chars);
	}
}