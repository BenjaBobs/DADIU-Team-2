using UnityEngine;
using System.Collections;

public class QADebugging : MonoBehaviour {

    public static QADebugging staticRef;
    bool hasLost = false;

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
            Application.LoadLevel(Application.loadedLevel);
            //GridSpawner.staticRef.ResetGrid();
        }

        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 25), "Exit"))
        {
            Application.Quit();
        }

        if (hasLost)
        {
            
            GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
            GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
            if (GUI.Button(new Rect(0, 25, Screen.width, Screen.height - 25), "Du har tabt! Reset?"))
            {
                Application.LoadLevel(Application.loadedLevel);
                //GridSpawner.staticRef.ResetGrid();
            }
        }
    }
}
