using UnityEngine;
using System.Collections;

public class guiCredits : MonoBehaviour {

    public static guiCredits staticref;
    private Texture background;
    private Texture credits;
    private GUISkin skinMenu;
    void Awake()
    {
        staticref = this;
    }
	// Use this for initialization
	void Start () {

        background = Resources.Load("GUI/StartScreenV3") as Texture;
        credits = Resources.Load("GUI/credits") as Texture;
        skinMenu = Resources.Load("GUI/GUIMenu") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skinMenu;
        float size = Screen.height / 20;
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), credits, ScaleMode.StretchToFill);
        if (GUI.Button(new Rect(Screen.width / 2 - (Screen.height - size * 8f) / 4f, Screen.height - size * 1f - 30f, (Screen.height - size * 8f) / 2f, 50f), Localization.instance.GetString(Localization.LocKey.ToMenu).ToUpper()))
        {
            Player.Reset();
            Application.LoadLevel(Application.loadedLevel);
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
        }
    }
}
