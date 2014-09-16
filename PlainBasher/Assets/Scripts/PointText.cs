using UnityEngine;
using System.Collections;

public class PointText : MonoBehaviour {

    public int scoreMultiplier = 1;
    public int scoreValue = 0;
    float timeLeft = 1;
    GUIText gText;


    void Start()
    {
        gText = gameObject.GetComponent<GUIText>();
    }

	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        pos.y += 0.03f * Settings.instance.GetDeltaTime();
        transform.localPosition = pos;
        gText.text = (scoreValue * scoreMultiplier).ToString();
        
        /*
        float redVal = 1;
        redVal -= Mathf.Log(scoreValue * scoreMultiplier)/10f;
        gText.color = new Color(redVal, redVal, redVal);
        */

        timeLeft -= Settings.instance.GetDeltaTime();
        if (timeLeft <= 0)
        {
            DestroyImmediate(gameObject);
            
        }
	}

}
