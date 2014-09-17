using UnityEngine;
using System.Collections;

public class TapCow : MonoBehaviour {
	public int cownum;

	void OnMouseDown () {
		if (cownum == 1)
			AudioManager.PlayCow1();
		else if (cownum == 2)
			AudioManager.PlayCow2();
		else if (cownum == 3)
			AudioManager.PlayCow3();
	}
}