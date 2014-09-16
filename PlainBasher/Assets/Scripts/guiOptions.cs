using UnityEngine;
using System.Collections;

public class guiOptions : MonoBehaviour {
    public static guiOptions staticref;
    private Texture background;
    public bool music = true;
    public bool sound = true;
    private bool bflag = true;
    private Texture flagUK;
    private Texture flagDK;
    private Texture flagUKnot;
    private Texture flagDKnot;
    private Texture flagUKSelected;
    private Texture flagDKSelected;
    private GUISkin skinMenu;
    private bool oldsound= true;
    private bool oldmusic = true;
	// Use this for initialization
    
    void Awake()
    {
        staticref = this;
    }

	void Start () {

        background = Resources.Load("GUI/StartScreenV2") as Texture;
        flagUKnot = Resources.Load("GUI/flagUK") as Texture;
        flagDKnot = Resources.Load("GUI/flagDK") as Texture;
        flagUKSelected = Resources.Load("GUI/flagUKselected") as Texture;
        flagDKSelected = Resources.Load("GUI/flagDKselected") as Texture;

        if (Localization.instance.GetLanguage())
        {
            bflag = true;
            flagUK = flagUKSelected;
            flagDK = flagDKnot;
        }
        else
        {
            bflag = false;
            flagUK = flagUKnot;
            flagDK = flagDKSelected;
        }
        skinMenu = Resources.Load("GUI/GUIMenu") as GUISkin;

	
	}
	
	// Update is called once per frame
	void Update () {

        if (sound != oldsound)
        {
            AudioManager.PlayButton();
            ToggleEffect();
            oldsound = sound;
            
        }

        if (music != oldmusic)
        {
            AudioManager.PlayButton();
            ToggleMusic();
            oldmusic = music;
            
        }

	}

    void ToggleMusic()
    {
        if (AudioManager.musicVolume == 1f)
        { music = false; AudioManager.ToggleMusic(); }
        else
        { music = true; AudioManager.ToggleMusic(); }
        Debug.Log("music: " + music.ToString() + "   " + AudioManager.musicVolume.ToString());

    }

    void ToggleEffect()
    {
        if (AudioManager.effectVolume == 1f)
        { sound = false; AudioManager.ToggleEffects(); }
        else
        { sound = true; AudioManager.ToggleEffects(); }
        Debug.Log("sound: " + sound.ToString() + "   " + AudioManager.effectVolume.ToString());
    }

    void OnGUI()
    {


        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
        GUI.skin = skinMenu;
        //GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height / 4 - 150, 600, 1000), ""); // fix when asset is added to guimain skin
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 4 -100, 200, 30), "Toggle "+ Localization.instance.GetString(Localization.LocKey.Music));
        music = GUI.Toggle(new Rect(Screen.width / 2 + 100 , Screen.height / 4 -100, 40, 40), music, "");

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 4 - 25, 200, 30), "Toggle " + Localization.instance.GetString(Localization.LocKey.Soundfx));
        sound = GUI.Toggle(new Rect(Screen.width / 2 + 100, Screen.height / 4 -25  , 40, 40), sound, "");

        if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/4 + 50,100,80),flagUK))
        {
            bflag = true;

                Localization.instance.SetLanguage(Localization.LocLanguage.English);
                flagUK = flagUKSelected;
                flagDK = flagDKnot;
                AudioManager.PlayButton();
        }

        if (GUI.Button(new Rect(Screen.width / 2 +50, Screen.height / 4 + 50, 100, 80), flagDK))
        {
            bflag = false;

                Localization.instance.SetLanguage(Localization.LocLanguage.Danish);
                flagUK = flagUKnot;
                flagDK = flagDKSelected;
                AudioManager.PlayButton();
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 4 + 250, 200, 100), Localization.instance.GetString(Localization.LocKey.ToMenu))) // localization
        {
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
            guiStart.staticref.enabled = true;
            this.enabled = false;
            AudioManager.PlayButton();
        }

        




    }
}
