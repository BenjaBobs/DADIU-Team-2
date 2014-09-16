﻿using UnityEngine;
using System.Collections;

public class guiStart : MonoBehaviour {
    public static guiStart staticref;
    private Texture background;
    private GUISkin skinMenu;
    private int butonWidth = 200;
    void Awake()
    {
        staticref = this;
    }

	// Use this for initialization
	void Start () {

        Settings.instance.SetPause(true);
        background = Resources.Load("GUI/loadingScreen") as Texture;
        skinMenu = Resources.Load("GUI/GUIMenu") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skinMenu;
        //Le´background, cause it is awsome
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleToFit);

        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height/4, 100, 30), Localization.instance.GetString(Localization.LocKey.PlayerName));
        //playerName = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 4 , 200, 30), playerName, 25);
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 50, butonWidth, 50), "START")) //no need for localization
        {
            Settings.instance.SetPause(false);
            this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 130, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Scoreboard).ToUpper()))
        {
            //gameObject.AddComponent("guiScore");
            //Destroy(this);
            guiScore.staticRef.enabled = true;
            this.enabled = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 210, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Options).ToUpper()))
        {
            //gameObject.AddComponent("guiOptions");
            //Destroy(this);
            guiOptions.staticref.enabled = true;
            this.enabled = false;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (butonWidth / 2), Screen.height / 4 + 290, butonWidth, 50), Localization.instance.GetString(Localization.LocKey.Quit).ToUpper()))
        {
            Application.Quit();
        }
    }
}
