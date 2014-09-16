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


                ScoreManager.LoadHighscore(); // load the HS
                sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
                if (sc)
                {
                    hs = ScoreManager.GetHighscore(); //get the score
                    if (ScoreManager.GetHighscore().Length > 0)
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

		GUI.Box (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2, size, Screen.height - size * 2, Screen.height - size * 2), ""); //No need for translation
		//add highscore content to box - get from highscore module
		GUI.Label (new Rect (Screen.width / 2 - 250, Screen.height / 10, 100, 25), Localization.instance.GetString (Localization.LocKey.Score) + ": " + Player.Score.ToString ());

		if (lhs.GetLength (0) > 0)
			GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10, 200, 50), Localization.instance.GetString (Localization.LocKey.Local) + " highscore: " + lhs [0].ToString ());



		if (flash)
				GUI.Label (new Rect (Screen.width / 2 + 100, Screen.height / 10, 200, 50), Localization.instance.GetString (Localization.LocKey.NPB));

		if (Player.Score > 0) {
			if (!uploaded) {
				GUI.Label (new Rect (Screen.width / 2 - 180, Screen.height / 10 + 25, 200, 30), Localization.instance.GetString (Localization.LocKey.PlayerName));

				playerName = GUI.TextField (new Rect (Screen.width / 2 - (Screen.height - size * 2) / 2f + 10f, size * 3 + 10f, (Screen.height - size * 2f) / 2f - 10f, 30f), playerName, 20);

				if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2 + (Screen.height - size * 2f) / 2f, size * 3, (Screen.height - size * 2f) / 2f, 50f), "Upload score")) {
					if (playerName.Length > 0) {
						ScoreManager.AddScore (playerName, Player.Score); //Add score
						uploaded = true;
					}
					else
						noName = true;
				}
				if (noNameflash)
					GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10 + 50, 200, 30), Localization.instance.GetString (Localization.LocKey.PlayerName));
			} else
				GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10 + 25, 200, 30), "Uploaded");
		} else
			GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 10 + 20, 200, 30), Localization.instance.GetString (Localization.LocKey.noUpload));

		int i = 0;

		if (hs.GetLength(0) > 0) {
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;
			foreach (string item in hs) {
				if (item != null && item.Length > 0)
					GUI.Label(new Rect (Screen.width / 2 - (Screen.height - size * 4) / 2, size * 5 + (size * 1.1f * i++), Screen.height - size * 4, 25), item.ToString()); //No need for translation
				else
					GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Fejl...");
			}
			sc2 = false; 
		}
		/*else {
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Loader...");
		}*/

		if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2 + (Screen.height - size * 2f) / 2f, Screen.height - size * 4, (Screen.height - size * 2f) / 2f, 50f), "Retry")) {
			AudioManager.StopSplashMusic();
			AudioManager.PlayButton();
			Settings.instance.SetPause(false);
			AudioManager.PlayMusic();
			this.enabled = false;
		}

		if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2, Screen.height - size * 4, (Screen.height - size * 2f) / 2f, 50f), "Return to menu")) {
			Player.Reset();
			Application.LoadLevel(Application.loadedLevel);
			//gameObject.AddComponent("guiStart");
			//Destroy(this);
		}
	}
}
