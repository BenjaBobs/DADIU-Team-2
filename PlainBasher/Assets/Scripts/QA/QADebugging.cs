using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QADebugging : MonoBehaviour {

    public static QADebugging staticRef;
    public bool godmode = false;

    GameObject prefabFreeez;
    GameObject prefabJelly;
    GameObject prefabElektro;
    GameObject prefabExplosion;



    float timeUsed = 0;

    Spawner spawnRef;

    void Awake()
    {
        staticRef = this;
    }

	// Use this for initialization
	void Start () {
        prefabFreeez = Resources.Load<GameObject>("Moles/Freeez");
        prefabExplosion = Resources.Load<GameObject>("Moles/Explosion");
        prefabElektro = Resources.Load<GameObject>("Moles/Elektro");
        prefabJelly = Resources.Load<GameObject>("Moles/Jelly");
	}
	
	// Update is called once per frame
	void Update () {
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

            if (GUI.Button(new Rect(220, 0, 100, 25), "Spawn Jelly"))
            {
                spawnRef.PlaceMole(prefabJelly);
            }
            if (GUI.Button(new Rect(320, 0, 100, 25), "Spawn Elektro"))
            {
                spawnRef.PlaceMole(prefabElektro);
            }
            if (GUI.Button(new Rect(420, 0, 120, 25), "Spawn Explosion"))
            {
                spawnRef.PlaceMole(prefabExplosion);
            }
            if (GUI.Button(new Rect(540, 0, 100, 25), "Spawn Freeez"))
            {
                spawnRef.PlaceMole(prefabFreeez);
            }

            if (GUI.Button(new Rect(660, 0, 100, 25), "Godmode " + (godmode ? "On" : "Off")))
            {
                godmode = !godmode;
            }

            if (GUI.Button(new Rect(800, 0, 100, 25), "Next Difficulty"))
            {
                Settings.instance.SkipDifficulty();
            }

            ////Player Information
            //GUI.Box(new Rect(8, 30, 102, 60), "Player Info");
            //GUI.Label(new Rect(10, 50, 100, 20), "Lives: " + Player.Lives);
            //GUI.Label(new Rect(10, 70, 100, 20), "Score: " + Player.Score);

            ////Spawn Rates
            //GUI.Box(new Rect(120, 30, 150, 60), "Spawner Rates");
            //GUI.Label(new Rect(130, 50, 120, 20), "Min Spawn time: " + (spawnRef.minFrequency * Settings.instance.GetDifficultySpawnRate()).ToString("0.0") + "s");
            //GUI.Label(new Rect(130, 70, 140, 20), "Max Spawn time: " + (spawnRef.maxFrequency * Settings.instance.GetDifficultySpawnRate()).ToString("0.0") + "s");
          
            //Difficulty info
            GUI.Box(new Rect(280, 30, 140, 60), "Difficulty info");
            GUI.Label(new Rect(290, 50, 140, 20), "Difficulty: " + Settings.instance.GetDifficultyIndex());
            GUI.Label(new Rect(290, 70, 140, 20), "Time: " + Settings.instance.GetGameTime().ToString("0.0") + "s");


        }
    }
}
