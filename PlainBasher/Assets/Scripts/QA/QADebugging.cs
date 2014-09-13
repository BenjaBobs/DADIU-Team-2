using UnityEngine;
using System.Collections;

public class QADebugging : MonoBehaviour {

    public static QADebugging staticRef;
    public bool hasLost = false;

    void Awake()
    {
        staticRef = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 25), "Reset"))
        {
            Player.Reset();
            Application.LoadLevel(Application.loadedLevel);
            //GridSpawner.staticRef.ResetGrid();
        }

        if (GUI.Button(new Rect(100, 0, 100, 25), "Pause"))
        {
            Settings.instance.TogglePause();
            //GridSpawner.staticRef.ResetGrid();
        }

        GUI.Label(new Rect(225, 0, 100, 25), "Lives: " + Player.Lives);
        GUI.Label(new Rect(325, 0, 100, 25), "Score: " + Player.score);

        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 25), "Exit"))
        {
            Application.Quit();
        }

        if (hasLost)
        {
            
            GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
            GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
            if (GUI.Button(new Rect(0, 25, Screen.width, Screen.height - 25), "Du har tabt! Score: " + Player.score))
            {
                Player.Reset();
                Application.LoadLevel(Application.loadedLevel);
                //GridSpawner.staticRef.ResetGrid();
            }
        }
    }
}
