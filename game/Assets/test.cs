using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
    public GameObject mole;
    private int count;
    public Vector3 startpos;
    public Vector3 endpos;
    public float speed = 6.0f;
    private float starttime;
    private float jouney;
    private int xnum = 20;
    private float xmod = 10f;

    private float updateInterval = 0.5f;
	private float lastInterval; // Last interval end time
	private float frames = 0; // Frames over current interval
	private float fps; // Current FPS
    public int row = 0;
    public bool tryk = false;
    public List<GameObject> kids;
	// Use this for initialization
	void Start () {

        startpos = (gameObject.transform.position);
        endpos = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        starttime = Time.time;
        jouney = Vector3.Distance(startpos, endpos);
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        kids = new List<GameObject>();
        
	}
	
	// Update is called once per frame
	void Update () {

        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }

        float distcovered = (Time.time - starttime) * speed;
        float frac = distcovered / jouney;
        transform.position = Vector3.Lerp(startpos, endpos, frac);
        if (transform.position == endpos)
        {
            Vector3 temp = endpos;
            endpos = startpos;
            startpos = temp;
            starttime = Time.time;
        }

	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 150f, 100f), "Spawn(" + count + ")"))
        {
            GameObject go;
            go = (GameObject)Instantiate(mole, new Vector3(count * 0.5f % xmod + transform.position.x, 0, -(row + count / xnum)),Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.particleSystem.enableEmission = tryk;
            kids.Add(go);
            count++;
            
   
        }

        if (GUI.Button(new Rect(0f, 200f, 150f, 100f), "SSpawn(" + count + ")"))
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject go;
                go = (GameObject)Instantiate(mole, new Vector3(count * 0.5f % xmod + transform.position.x, 0, -(row + count / xnum)), Quaternion.identity);
                go.transform.parent = gameObject.transform;
                go.particleSystem.enableEmission = tryk;
                kids.Add(go);
                count++;
            }
        }


        if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "quit"))
        {
            Application.Quit();
        }

        GUI.Label(new Rect(400f, 0f, 50f, 25f), (1 / Time.deltaTime).ToString());
        GUI.Label( new Rect(500f,0,50f,25f), fps.ToString("f3"));

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "particles(" + tryk.ToString() + ")"))
        {
            tryk =! tryk;
            foreach (GameObject kid in kids )
            {
                kid.particleSystem.enableEmission = tryk;
            }
        }
    }
}
