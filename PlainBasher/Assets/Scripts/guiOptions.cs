using UnityEngine;
using System.Collections;

public class guiOptions : MonoBehaviour {
    public static guiOptions staticref;
    private Texture background;
    public bool music = true;
    public bool sound = true;
    private bool bflag = true;
    private Texture flag;
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
        flag = flagUK;


	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleToFit);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 4 -100, 150, 30), "Toggle "+ Localization.instance.GetString(Localization.LocKey.Music));
        music = GUI.Toggle(new Rect(Screen.width / 2 + 50 , Screen.height / 4 -100, 40, 40), music, "");

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 4 - 25, 150, 30), "Toggle " + Localization.instance.GetString(Localization.LocKey.Soundfx));
        sound = GUI.Toggle(new Rect(Screen.width / 2 + 50, Screen.height / 4 -25  , 40, 40), sound, "");

        if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/4 + 50,200,200),flag))
        {
            bflag = !bflag;
            flag = bflag ? flagUK : flagDK;
            if (bflag)
            {
                Localization.instance.SetLanguage(Localization.LocLanguage.English);
            }
            else
            {
                Localization.instance.SetLanguage(Localization.LocLanguage.Danish);
            }
            
        }

        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 + 250, 100, 100), "Return to menu"))
        {
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
            guiStart.staticref.enabled = true;
            this.enabled = false;
        }

        




    }
}
