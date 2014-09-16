using UnityEngine;
using System.Collections;

public class guiStart : MonoBehaviour {
    public static guiStart staticref;
    private Texture background;

    void Awake()
    {
        staticref = this;
    }

	// Use this for initialization
	void Start () {

        Settings.instance.SetPause(true);
        background = Resources.Load("GUI/loadingScreen") as Texture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        //Le´background, cause it is awsome
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleToFit);

        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height/4, 100, 30), Localization.instance.GetString(Localization.LocKey.PlayerName));
        //playerName = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 4 , 200, 30), playerName, 25);
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 +50, 100, 50), "START")) //localization ?
        {
			AudioManager.StopSplashMusic(true);
			AudioManager.PlayButton();
            Settings.instance.SetPause(false);
			AudioManager.PlayMusic();
			this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 + 100, 100, 50), Localization.instance.GetString(Localization.LocKey.Scoreboard)))
        {
			AudioManager.PlayButton();
            //gameObject.AddComponent("guiScore");
            //Destroy(this);
            guiScore.staticRef.enabled = true;
            this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 + 150, 100, 50), Localization.instance.GetString(Localization.LocKey.Options)))
        {
			AudioManager.PlayButton();
            //gameObject.AddComponent("guiOptions");
            //Destroy(this);
            guiOptions.staticref.enabled = true;
            this.enabled = false;
        }

        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 + 200, 100, 50), Localization.instance.GetString(Localization.LocKey.Quit)))
        {
			AudioManager.PlayButton();
            Application.Quit();
        }
    }
}
