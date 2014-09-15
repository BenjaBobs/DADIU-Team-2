using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.PlayHoldFreeze();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
			AudioManager.StopHoldFreeze();
		if (Input.GetKeyDown(KeyCode.H))
			AudioManager.PlayHoldFreeze();
		if (Input.GetKeyDown(KeyCode.J))
			AudioManager.ToggleEffects();
	}


}