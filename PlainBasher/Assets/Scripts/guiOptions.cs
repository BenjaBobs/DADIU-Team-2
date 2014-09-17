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
    private GUISkin menuSkin;
    private bool oldsound= true;
    private bool oldmusic = true;
	// Use this for initialization
    
    void Awake()
    {
        staticref = this;
    }

	void Start () {

        background = Resources.Load("GUI/StartScreenV3") as Texture;
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
        menuSkin = Resources.Load("GUI/GUIMenu") as GUISkin;

	
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

        float size = Screen.height / 20;
        GUI.skin = menuSkin;
        GUI.skin.label.fontSize = (int)size;
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);

        // Background
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
        GUI.Box(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2, size, Screen.height - size * 2, Screen.height - size * 2), "");

        // Overskrift: Highscore
        GUI.skin.label.fontSize = (int)size * 2;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2, size, Screen.height - size * 2, size * 4), Localization.instance.GetString(Localization.LocKey.Options));
        GUI.skin.label.fontSize = (int)size;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2 + 20f, size * 4.6f, Screen.height - size * 2, size * 2), Localization.instance.GetString(Localization.LocKey.Toggle) + " " + Localization.instance.GetString(Localization.LocKey.Music));

        music = GUI.Toggle(new Rect(Screen.width - (Screen.height - size * 2) + 20f + size * 4, size * 4.6f, Screen.height - size * 18, size * 2), music, "");

        GUI.Label(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2 + 20f, size * 6.6f, Screen.height - size * 2, size * 2), Localization.instance.GetString(Localization.LocKey.Toggle) + " " + Localization.instance.GetString(Localization.LocKey.Soundfx));
        sound = GUI.Toggle(new Rect(Screen.width - (Screen.height - size * 2) + 20f + size * 4, size * 6.6f, Screen.height - size * 18, size * 2), sound, "");

        if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 2) / 2 + 20f + size * 6, size * 10.6f, 100, 80), flagUK))
        {
            bflag = true;

                Localization.instance.SetLanguage(Localization.LocLanguage.English);
                flagUK = flagUKSelected;
                flagDK = flagDKnot;
                AudioManager.PlayButton();
        }

        if (GUI.Button(new Rect(Screen.width - (Screen.height - size * 2) + 20f + size * 3, size * 10.6f, 100, 80), flagDK))
        {
            bflag = false;

                Localization.instance.SetLanguage(Localization.LocLanguage.Danish);
                flagUK = flagUKnot;
                flagDK = flagDKSelected;
                AudioManager.PlayButton();
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 8f) / 4f, Screen.height - size * 1.5f - 50f, (Screen.height - size * 8f) / 2f, 50f), Localization.instance.GetString(Localization.LocKey.ToMenu))) // localization
        {
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
            guiStart.staticref.enabled = true;
            this.enabled = false;
            AudioManager.PlayButton();
        }

        




    }
}
