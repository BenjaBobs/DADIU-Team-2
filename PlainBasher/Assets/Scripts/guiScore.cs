using UnityEngine;
using System.Collections;



public class guiScore : MonoBehaviour {
    public static guiScore staticRef;
    public Vector3 flashPo = new Vector3(0.6f, 0.98f, 0f);
    public int flashSize = 20;
    public Color flashColor = new Color(255f,0f,0f);
    private bool flash = false;
    private float flashtimer;
    private bool newBest = false;
    public bool sc = false;
    private bool sc2 = true;
    private string[] hs = {};
    private int[] lhs;
    public GUISkin menuSkin;
    public string playerName = "";
    private bool uploaded;
    private bool noName = false;
	private bool noNameflash = false;
	private int noNameCount = 0;

    void Awake() {
        staticRef = this;
    }

	void Start () {
		ScoreManager.UnsetHighscore();

        menuSkin = Resources.Load("GUI/GUIMenu") as GUISkin;
        uploaded = false;

        
        if (Player.Score == 0)
        {
            ScoreManager.LoadHighscore(); // load the HS
            sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
            uploaded = true;
        }

        
        

        lhs = ScoreManager.GetLocalHighscore();
        flashtimer = Time.time;

        if (lhs.GetLength(0) > 0)
        {
            if (Player.Score > lhs[0])
            {
                newBest = true;
                ScoreManager.SaveLocalScore(Player.Score);
            }
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (newBest == true)
        {
            if (flashtimer < Time.time)
            {
                flashtimer = Time.time + 0.5f;
                flash = !flash;
            }
        }
        if (uploaded)
        {
            if (sc2)
            {


                //ScoreManager.LoadHighscore(); // load the HS
                sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
                if (sc)
                {
                    hs = ScoreManager.GetHighscore(); //get the score
					if (hs.Length > 0)
                        sc = false;
                    
                }
            }
        }
		if (noName)
        {
            
            if (flashtimer < Time.time)
            {
                flashtimer = Time.time + 0.4f;
				noNameflash = !noNameflash;
                noNameCount++;

                if (noNameCount > 10)
                {
                    noNameCount = 0;
                    noName = false;
                    noNameflash = false;

                }
                
            }
        }
    }

	void OnGUI() {
		float size = Screen.height / 20;
		GUI.skin = menuSkin;
		GUI.skin.label.fontSize = (int)size;
		GUI.color = new Color (1.0f, 1.0f, 1.0f, 0.9f);

		// Background
		GUI.Box (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2, size, Screen.height - size * 2, Screen.height - size * 2), "");

		// Overskrift: Highscore
		GUI.skin.label.fontSize = (int)size * 3;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2, size, Screen.height - size * 2, size * 4), "Highscore");
		GUI.skin.label.fontSize = (int)size;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;








		if (Player.Score > 0) {
			if (!uploaded) {
				//ScoreManager.UnsetHighscore();

				// Show score
				GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2 + 20f, size * 4.6f, Screen.height - size * 2, size * 2), Localization.instance.GetString (Localization.LocKey.Score) + ": " + Player.Score.ToString ());
				
				if (lhs.GetLength (0) > 0)
					GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2 + 20f, size * 6.6f, Screen.height - size * 2, size * 2), Localization.instance.GetString (Localization.LocKey.Local) + " highscore: " + lhs [0].ToString ());

				// Local best
				if (flash) {
					GUI.skin.label.alignment = TextAnchor.MiddleRight;
					GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2 - 20f, size * 4.6f, Screen.height - size * 2, size * 2), Localization.instance.GetString (Localization.LocKey.NPB));
				}
				GUI.skin.label.alignment = TextAnchor.MiddleLeft;

				// Player name
				GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2f + 20f, size * 8.6f, (Screen.height - size * 2f) / 2f - 10f, size * 2), Localization.instance.GetString (Localization.LocKey.PlayerName) + ":");

				playerName = GUI.TextField (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2f + 10f, size * 10 + 10f, (Screen.height - size * 2f) / 2f - 10f, 30f), playerName, 20);

				if (GUI.Button(new Rect(Screen.width / 2 + size * 1f, size * 10, (Screen.height - size * 8f) / 2f, 50f), "Upload score".ToUpper())) {
					if (playerName.Length > 0) {
						ScoreManager.AddScore (playerName, Player.Score); //Add score
						uploaded = true;
					}
					else
						noName = true;
				}
				if (noNameflash)
					GUI.Label (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2f + 20f, size * 10.6f, (Screen.height - size * 2f) / 2f - 10f, size * 2), Localization.instance.GetString (Localization.LocKey.PlayerName));
			}
			else {





				//GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10 + 25, 200, 30), "Uploaded");
			}
		}
		//else
			//GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10 + 20, 200, 30), Localization.instance.GetString (Localization.LocKey.noUpload));

		int i = 0;

		if (hs.GetLength(0) > 0) {
			GUI.skin.label.alignment = TextAnchor.UpperLeft;
			foreach (string item in hs) {
				if (item != null && item.Length > 0)
					GUI.Label(new Rect (Screen.width / 2 - (Screen.height - size * 4) / 2, size * 5.5f + (size * 1.1f * i++) - 10, Screen.height - size * 4, size * 2), item.ToString()); //No need for translation
				else
					GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Fejl...");
			}
			sc2 = false;
		}
		/*else {
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Loader...");
		}*/

		/*if (GUI.Button(new Rect(Screen.width / 2 + size * 1f, Screen.height - size * 1.5f - 50f, (Screen.height - size * 8f) / 2f, 50f), "Retry".ToUpper())) {
			AudioManager.StopSplashMusic();
			AudioManager.PlayButton();
			Settings.instance.SetPause(false);
			AudioManager.PlayMusic();
			this.enabled = false;
		}

		if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 6f) / 2, Screen.height - size * 1.5f - 50f, (Screen.height - size * 8f) / 2f, 50f), "Return to menu".ToUpper())) {
			Player.Reset();
			Application.LoadLevel(Application.loadedLevel);
			//gameObject.AddComponent("guiStart");
			//Destroy(this);
		}*/

		if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 8f) / 4f, Screen.height - size * 1.5f - 50f, (Screen.height - size * 8f) / 2f, 50f), Localization.instance.GetString (Localization.LocKey.ToMenu).ToUpper())) {
			Player.Reset();
			Application.LoadLevel(Application.loadedLevel);
			//gameObject.AddComponent("guiStart");
			//Destroy(this);
		}
	}
}
