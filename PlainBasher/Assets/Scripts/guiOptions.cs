using UnityEngine;
using System.Collections;

public class guiOptions : MonoBehaviour {
    public static guiOptions staticref;
    private Texture background;
    public bool music = true;
    public bool sound = true;
    private Texture flagUK;
    private Texture flagDK;
	// Use this for initialization

    void Awake()
    {
        staticref = this;
    }

	void Start () {

        background = Resources.Load("GUI/loadingScreen") as Texture;
        flagUK = Resources.Load("GUI/flagUK") as Texture;
        flagDK = Resources.Load("GUI/flagDK") as Texture;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI()
    {
		float posx;
		posx = Screen.width / 2;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleToFit);

		GUI.Label(new Rect(posx - 75, Screen.height / 4 -100, 150, 30), "Toggle "+ Localization.instance.GetString(Localization.LocKey.Music));
        music = GUI.Toggle(new Rect(posx + 50 , Screen.height / 4 -100, 40, 40), music, "");

		GUI.Label(new Rect(posx - 75, Screen.height / 4 - 25, 150, 30), "Toggle " + Localization.instance.GetString(Localization.LocKey.Soundfx));
        sound = GUI.Toggle(new Rect(posx + 50, Screen.height / 4 -25  , 40, 40), sound, "");

		if (GUI.Button(new Rect(posx - 100 - 150, Screen.height/4 + 50,200,200),flagUK))
        {
			Localization.instance.SetLanguage(Localization.LocLanguage.English);
			AudioManager.PlayButton();
        }
		if (GUI.Button(new Rect(posx - 100 + 150, Screen.height/4 + 50,200,200),flagDK))
		{
			Localization.instance.SetLanguage(Localization.LocLanguage.Danish);
			AudioManager.PlayButton();
		}

		if (GUI.Button(new Rect(posx- 75, Screen.height *0.8f, 150, 75), Localization.instance.GetString(Localization.LocKey.ReturnToMenu)))
        {
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
            guiStart.staticref.enabled = true;
            this.enabled = false;
			AudioManager.PlayButton();
        }

        




    }
}
