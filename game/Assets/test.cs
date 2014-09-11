using UnityEngine;
using System.Collections;

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


	// Use this for initialization
	void Start () {

        startpos = (gameObject.transform.position);
        endpos = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        starttime = Time.time;
        jouney = Vector3.Distance(startpos, endpos);
	}
	
	// Update is called once per frame
	void Update () {
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
            ((GameObject)Instantiate(mole, new Vector3(count * 0.5f %xmod + transform.position.x , 0, -(count / xnum)), Quaternion.identity)).transform.parent = gameObject.transform;
            count++;
   
        }

        if (GUI.Button(new Rect(0f, 200f, 150f, 100f), "SSpawn(" + count + ")"))
        {
            for (int i = 0; i < 10; i++)
            {


                ((GameObject)Instantiate(mole, new Vector3(count * 0.5f % xmod + transform.position.x, 0, -(count / xnum)), Quaternion.identity)).transform.parent = gameObject.transform;
            count++;
            }
        }


        if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "quit"))
        {
            Application.Quit();
        }

    }
}
