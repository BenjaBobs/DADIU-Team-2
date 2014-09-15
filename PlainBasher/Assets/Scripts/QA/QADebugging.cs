using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QADebugging : MonoBehaviour {

    public static QADebugging staticRef;
    public bool hasLost = false;

    float timeUsed = 0;

    Spawner spawnRef;

    void Awake()
    {
        staticRef = this;
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timeUsed += Settings.instance.GetDeltaTime();
        if (!spawnRef)
            spawnRef = Spawner.DBGstaticRef;
	}

    void OnGUI()
    {
        if (spawnRef)
        {
            spawnRef.Debug();
            //QA Buttons
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

            //Player Information
            GUI.Box(new Rect(8, 30, 102, 60), "Player Info");
            GUI.Label(new Rect(10, 50, 100, 20), "Lives: " + Player.Lives);
            GUI.Label(new Rect(10, 70, 100, 20), "Score: " + Player.Score);

            //Spawn Chances
            GUI.Box(new Rect(8, 100, 170, 100), "Spawn Chances");
            GUI.Label(new Rect(10, 120, 170, 20), "Jelly chance: " + spawnRef.DBGjellyChance.ToString("P1"));
            GUI.Label(new Rect(10, 140, 170, 20), "Freeez chance: " + spawnRef.DBGfreeezChance.ToString("P1"));
            GUI.Label(new Rect(10, 160, 170, 20), "Elektro chance: " + spawnRef.DBGelektroChance.ToString("P1"));
            GUI.Label(new Rect(10, 180, 170, 20), "Explosion chance: " + spawnRef.DBGexplosionChance.ToString("P1"));

            //Spawn Rates
            GUI.Box(new Rect(200, 30, 170, 60), "Spawner Rates");
            GUI.Label(new Rect(210, 50, 170, 20), "Min Spawn time: " + (spawnRef.minFrequency * Settings.instance.GetDifficultySpawnRate()).ToString("0.0") + "s");
			GUI.Label(new Rect(210, 70, 170, 20), "Max Spawn time: " + (spawnRef.maxFrequency * Settings.instance.GetDifficultySpawnRate()).ToString("0.0") + "s");

            //Difficulty info
            GUI.Box(new Rect(200, 100, 170, 60), "Difficulty info");
            GUI.Label(new Rect(210, 120, 170, 20), "Difficulty: " + Settings.instance.GetDifficultyIndex(timeUsed));
            GUI.Label(new Rect(210, 140, 170, 20), "Time: " + timeUsed.ToString("0.0") + "s");


            if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 25), "Exit"))
            {
                Application.Quit();
            }

            if (hasLost)
            {
            
                GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
                GUI.Box(new Rect(0, 25, Screen.width, Screen.height - 25), "");
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), "Du har tabt!");
            }
        }
    }
}
