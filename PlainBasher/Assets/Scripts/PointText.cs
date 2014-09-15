using UnityEngine;
using System.Collections;

public class PointText : MonoBehaviour {

    float timeLeft = 1;

	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        pos.y += 0.03f * Settings.instance.GetDeltaTime();
        transform.localPosition = pos;

        timeLeft -= Settings.instance.GetDeltaTime();
        if (timeLeft <= 0)
            DestroyImmediate(gameObject);
	}
}
