using UnityEngine;
using System.Collections;

public class GroundClicker : MonoBehaviour {

	private void OnMouseDown() {
		if (!Settings.instance.GetPaused())
			AudioManager.PlayTapGround();
	}
}