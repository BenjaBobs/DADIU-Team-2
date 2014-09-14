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
    public bool sc = false;
    private string[] hs;
    public GUISkin menuSkin;
	// Use this for initialization
	void Start () {
        menuSkin = Resources.Load("GUI/GUIMenu") as GUISkin;
        if (Player.score > 0)
        {
            ScoreManager.AddScore(Player.name, Player.score);  //Add score
        }
            ScoreManager.LoadHighscore(); // load the HS
            sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
        


        score = 200;
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

        if (sc)
        {
            sc = false;
            
            hs = ScoreManager.GetHighscore(); //get the score

            foreach (string item in hs)
            {
                Debug.Log(item.ToString());                   
            }
            
        }
	}

    void OnGUI()
    {
        
        GUI.Box(new Rect(Screen.width /2 - 250, Screen.height/10 + 50, 400, 300), "Highscore menu");  //No need for translation
        //add highscore content to box - get from highscore module
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height/10, 100, 25), Localization.instance.GetString(Localization.LocKey.Score)+":");
        GUI.Label(new Rect(Screen.width / 2 -50, Screen.height/10 , 100, 25), score.ToString());

        int i = 0;
        foreach (string item in hs)
        {
            GUI.Label(new Rect(Screen.width / 2 - 225, Screen.height / 10 + 100 + (25 * i), 450, 25), item.ToString());
            i++;
        }

        if (GUI.Button(new Rect(Screen.width/2, Screen.height/10 + 350, 100, 100), "Return to menu"))
        {
            gameObject.AddComponent("StartMenu");
            Destroy(newBestFlash);
            Destroy(this);
        }

    }
}
