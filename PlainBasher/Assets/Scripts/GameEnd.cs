using UnityEngine;
using System.Collections;



public class GameEnd : MonoBehaviour {

    public int score;
    public Vector3 flashPo = new Vector3(0.6f, 0.98f, 0f);
    public int flashSize = 20;
    public Color flashColor = new Color(255f,0f,0f);
    private int bestScore;
    public GameObject newBestFlash;
    private bool flash = false;
    private float flashtimer;
    private bool newBest = false;
	// Use this for initialization
	void Start () {

        score = 500;
        bestScore = 0; // get from highscore method.
        newBestFlash = new GameObject();
        newBestFlash.name = "FlahsingGuiText_NewBestScore";
        newBestFlash.AddComponent<GUIText>();
        newBestFlash.guiText.text = Localization.instance.GetString(Localization.LocKey.NPB);
        newBestFlash.guiText.color = flashColor;
        newBestFlash.guiText.fontSize = flashSize;
        newBestFlash.guiText.enabled = false;
        newBestFlash.transform.position = flashPo;
        
        flashtimer = Time.time;
        if (score > bestScore)
        {
            newBest = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (newBest == true)
        {
            if (flashtimer < Time.time)
            {
                flashtimer = Time.time + 0.5f;
                flash = !flash;
                newBestFlash.guiText.enabled = flash;             
            } 
        }

    //for testing only - remove to optimize for release
        if (Debug.isDebugBuild)
        {
            newBestFlash.guiText.color = flashColor;
            newBestFlash.guiText.fontSize = flashSize;
            newBestFlash.transform.position = flashPo;
        }

	}

    void OnGUI()
    {
        GUI.Box (new Rect (50,50,500,500), "Highscore menu"); //No need for translation
        //add highscore content to box - get from highscore module
        GUI.Label(new Rect(Screen.width / 2 - 100, 10, 100, 25), Localization.instance.GetString(Localization.LocKey.Score)+":");
        GUI.Label(new Rect(Screen.width / 2 -50, 10, 100, 25), score.ToString());
                     

        if (GUI.Button(new Rect(200, 200, 100, 100), "KILL")) // debug
        {
            Destroy(newBestFlash);
            Destroy(this);
        }
    }
}
