using UnityEngine;
using System.Collections;

public class deleteme : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(500, 100, 100, 50), "start"))
        {
            gameObject.AddComponent("GameEnd");
        }
    }
}
