using UnityEngine;
using System.Collections;

public class guiStart : MonoBehaviour {
    public static guiStart staticref;
    private Texture background;
    private GUISkin skinMenu;
    private int butonWidth = 200;
    private GUIStyle textstyle;
    void Awake()
    {
        staticref = this;
    }

	// Use this for initialization
	void Start () {

        Settings.instance.SetPause(true);
        background = Resources.Load("GUI/StartScreenV4") as Texture;
        skinMenu = Resources.Load("GUI/GUIMenu") as GUISkin;
        textstyle = new GUIStyle();
        textstyle.normal.textColor = new Color((137f / 256f), (59f / 256f), (115f / 256f));
        textstyle.fontSize = 30;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        
        //Le´background, cause it is awsome
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
        GUI.skin = skinMenu;
        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height/4, 100, 30), Localization.instance.GetString(Localization.LocKey.PlayerName));
        //playerName = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 4 , 200, 30), playerName, 25);
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 50, butonWidth, 50), "START")) //no need for localization
        {

			AudioManager.StopSplashMusic();
			AudioManager.PlayButton();
            Settings.instance.SetPause(false);
			AudioManager.PlayMusic();
            if (guiPlayer.staticref)
            {
                guiPlayer.staticref.enabled = true;
            }


            this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 130, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Scoreboard).ToUpper()))
        {
			AudioManager.PlayButton();
            //gameObject.AddComponent("guiScore");
            //Destroy(this);
            guiScore.staticRef.enabled = true;
            this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 210, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Options).ToUpper()))
        {
			AudioManager.PlayButton();
            //gameObject.AddComponent("guiOptions");
            //Destroy(this);
            guiOptions.staticref.enabled = true;
            this.enabled = false;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 370, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Quit).ToUpper()))
        {
			AudioManager.PlayButton();
            Application.Quit();
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 290, butonWidth, 50), "CREDITS"))
        {
            AudioManager.PlayButton();
            if (guiCredits.staticref)
            {
                guiCredits.staticref.enabled = true;
                this.enabled = false;
            }
        }

        //GUI.skin.label.fontSize = 30;
        //GUI.skin.label.normal.textColor = new Color((216f/256f), (81f/256f), (205f/256f));
        //GUI.skin.label.normal.textColor = new Color((137f/256f), (59f/256f), (115f/256f));
        //GUI.skin.label.onNormal.textColor = new Color(216f, 81f, 205f);
        //GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
        GUI.Label(new Rect(Screen.width - butonWidth - 400, Screen.height - 50, butonWidth + 400, 60), Localization.instance.GetString(Localization.LocKey.Madeby),textstyle);
    }
}
