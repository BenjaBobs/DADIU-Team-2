using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.PlayFreeze();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
			AudioManager.StopFreeze();
		if (Input.GetKeyDown(KeyCode.H))
			AudioManager.PlayFreeze();
		if (Input.GetKeyDown(KeyCode.J))
			AudioManager.ToggleEffects();
	}


}