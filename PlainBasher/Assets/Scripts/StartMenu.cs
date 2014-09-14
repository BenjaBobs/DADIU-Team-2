using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    public bool toggleLang = false;
    public string playerName =""; //acess Player.name (instance) or send to Player instace on start
    private Texture background;

	// Use this for initialization
	void Start () {

        background = Resources.Load("GUI/loadingScreen") as Texture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        //Le´background, cause it is awsome
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleToFit);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height/4, 100, 30), "Player Name:");
       playerName = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 4 , 200, 30), playerName, 25);
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 +50, 100, 50), "START"))
        {
            //
        }
        //See highscore button ?
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 +100, 100, 50), "Highscores"))
        {
            gameObject.AddComponent("GameEnd");
            Destroy(this);
        }
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4 + 150, 100, 50), "EXIT"))
        {
            Application.Quit();
        }

    }
}
