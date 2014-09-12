using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    public float difficultyMultiplier = 1.0f;
    public float difficultyTimeMultiplier = 0.1f;

    public static Settings staticRef;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        staticRef = this;
    }
}
