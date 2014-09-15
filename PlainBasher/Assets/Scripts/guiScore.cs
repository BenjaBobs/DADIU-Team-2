using UnityEngine;
using System.Collections;



public class guiScore : MonoBehaviour {
    public static guiScore staticRef;
    public Vector3 flashPo = new Vector3(0.6f, 0.98f, 0f);
    public int flashSize = 20;
    public Color flashColor = new Color(255f,0f,0f);
    private bool flash = false;
    private float flashtimer;
    private bool newBest = false;
    public bool sc = false;
    private bool sc2 = true;
    private string[] hs = {};
    private int[] lhs;
    public GUISkin menuSkin;
    public string playerName = "";
    private bool uploaded;
    private bool retard = false;
    private bool retardflash = false;
    private int retardcount = 0;
	// Use this for initialization

    void Awake()
    {
        staticRef = this;
    }

	void Start () {
        
        menuSkin = Resources.Load("GUI/GUIMenu") as GUISkin;
        uploaded = false;

        
        if (Player.Score == 0)
        {
            ScoreManager.LoadHighscore(); // load the HS
            sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
            uploaded = true;
        }

        
        

        lhs = ScoreManager.GetLocalHighscore();
        flashtimer = Time.time;

        if (lhs.GetLength(0) > 0)
        {
            if (Player.Score > lhs[0])
            {
                newBest = true;
            }
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (newBest == true)
        {
            if (flashtimer < Time.time)
            {
                flashtimer = Time.time + 0.5f;
                flash = !flash;
            }
        }
        if (uploaded)
        {
            if (sc2)
            {


                ScoreManager.LoadHighscore(); // load the HS
                sc = ScoreManager.IsHighscoreLoaded(); // did it get loaded?
                if (sc)
                {
                    hs = ScoreManager.GetHighscore(); //get the score
                    if (ScoreManager.GetHighscore().Length > 0)
                        sc = false;
                    
                }
            }
        }
        if (retard)
        {
            
            if (flashtimer < Time.time)
            {
                flashtimer = Time.time + 0.4f;
                retardflash = !retardflash;
                retardcount++;

                if (retardcount > 10)
                {
                    retardcount = 0;
                    retard = false;
                    retardflash = false;

                }
                
            }
        }
    }

    void OnGUI()
    {

        

        GUI.Box(new Rect(Screen.width /2 - 250, Screen.height/10 + 75, 400, 300), "Highscore menu");  //No need for translation
        //add highscore content to box - get from highscore module
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height/10, 100, 25), Localization.instance.GetString(Localization.LocKey.Score)+": " + Player.Score.ToString());

        if (lhs.GetLength(0) > 0)
        {
        GUI.Label(new Rect(Screen.width / 2 -100 , Screen.height / 10, 200, 50), Localization.instance.GetString(Localization.LocKey.Local) +" highscore: "+ lhs[0].ToString());

        }



        if (flash)
        {
            GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 10, 200, 50), Localization.instance.GetString(Localization.LocKey.NPB));
        }
       
        if (Player.Score > 0)
        {

            if (!uploaded)
            {
                GUI.Label(new Rect(Screen.width / 2 - 180, Screen.height / 10 + 25, 200, 30), Localization.instance.GetString(Localization.LocKey.PlayerName));
                playerName = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 10 + 20, 150, 30), playerName, 20);

                if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 10 + 20, 100, 30), "Upload score"))
                {
                    if (playerName.Length > 0)
                    {
                        ScoreManager.AddScore(playerName, Player.Score);  //Add score
                        uploaded = true;
                    }
                    else
                    {
                        retard = true;
                    }


                }
                if (retardflash)
                {
                  GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 10 + 50, 200, 30), Localization.instance.GetString(Localization.LocKey.PlayerName));  
                }
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2 - 100 , Screen.height / 10 + 25, 200, 30), "Uploaded");
            }
            
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 10 + 20, 200, 30), Localization.instance.GetString(Localization.LocKey.noUpload));
        }
        int i = 0;

        if (hs.GetLength(0) > 0)
        {
            foreach (string item in hs)
            {
                GUI.Label(new Rect(Screen.width / 2 - 225, Screen.height / 10 + 100 + (25 * i), 450, 25), item.ToString());
                i++;
                sc2 = false;
            }
        }

        //if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 10 + 400, 100, 100), "Retry"))
        //{
        //    //close gui and start game
        //    Destroy(this);
        //}

        if (GUI.Button(new Rect(Screen.width/2, Screen.height/10 + 400, 100, 100), "Return to menu"))
        {
            Player.Reset();
            Application.LoadLevel(Application.loadedLevel);
            //gameObject.AddComponent("guiStart");
            //Destroy(this);
        }

    }
}
