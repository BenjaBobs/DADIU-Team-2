using UnityEngine;
using System.Collections;
using System;

// Add score:
// ScoreManager.AddScore ("Navn", 0);

// Get highscore array
// highscore = ScoreManager.GetHighscore();

// Extract names and scores from highscore array
// string[] navne = new string[test.Length/2];
// string[] score = new string[test.Length/2];
// for (int i = 0; i < test.Length/2; i++) {
//     navne[i] = test[i,0];
//     score[i] = test[i,1];
// }

public class ScoreManager : MonoBehaviour {
	// Database connection
	protected readonly string baseURL = "http://chx.dk/connect/dadiu.php";
	protected readonly string highscoreURL = "http://chx.dk/connect/dadiu.php?highscore=1";
	private static bool uploaded = false;
	private static bool highscoreLoaded = false;
	private static string[,] highscore = new string[0,0];

	// Singleton
	public static ScoreManager instance;

	private void Awake() {
		if (instance == null)
			instance = this;
	}

	public static void LoadHighscore() {
		if (!highscoreLoaded) {
			highscoreLoaded = true;
			instance.StartCoroutine (instance.GetHighscoreData ());
		}
	}
	
	public static string[,] GetHighscore() {
		return highscore;
	}

	public static void AddScore(string name, int score) {
		instance.StartCoroutine(instance.Database(name, score));
	}

	private void ExtractToArray(string data) {
		string[] persons = data.Split(new string[] { ";SPLIT;" }, StringSplitOptions.None);
		highscore = new string[persons.Length,2];

		for (int i = 0; i < persons.Length; ++i) {
			string[] information = persons[i].Split(new string[] { ",SPLIT," }, StringSplitOptions.None);
			highscore[i,0] = information[0];
			highscore[i,1] = information[1];
		}
	}

	private IEnumerator GetHighscoreData() {
		WWW www = null;
		
		int index = 0;
		while (www == null && index < 20 && !uploaded) {
			index++;
			
			www = new WWW(highscoreURL);
			
			yield return www;
		}

		if (www.text != null) {
			ExtractToArray(www.text);
			highscoreLoaded = false;
		}
	}

	private IEnumerator Database(string name, int score) {
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
			highscore = new string[0,0];
			Debug.Log ("uploaded");
		}
		else
			Debug.Log ("not uploaded");
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