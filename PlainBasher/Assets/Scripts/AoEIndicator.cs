using UnityEngine;
using System.Collections;

public class AoEIndicator : MonoBehaviour {

    [SerializeField]
    Color explosionColor;
    [SerializeField]
    Color explosionInactiveColor;
    [SerializeField]
    Color elektroColor;
    [SerializeField]
    Color elektroInactiveColor;

    [SerializeField]
    float fadeTime;

    
    public bool explosionIndicator = false;
    public bool elektroIndicator = false;
    bool fadeIn = true;

    float colorPercentage = 0f;

    static float gStartTime;
    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {

        if (Time.time - startTime >= fadeTime)
        {
            fadeIn = !fadeIn;
            startTime = Time.time;
        }

        colorPercentage = fadeIn ? (Time.time - startTime) / fadeTime : 1f - (Time.time - startTime) / fadeTime;
        
	    if (explosionIndicator)
        {
            
            if (elektroIndicator) //Both active
            {
                gameObject.renderer.material.color = Color.Lerp(explosionColor, elektroColor, colorPercentage);
            }
            else //Explosion active
            {
                gameObject.renderer.material.color = Color.Lerp(explosionInactiveColor, explosionColor, colorPercentage);
            }

        }
        else if (elektroIndicator) //Elektro active
        {
            gameObject.renderer.material.color = Color.Lerp(elektroInactiveColor, elektroColor, colorPercentage);
        }
	}

    public void IndicateExplosion()
    {
        explosionIndicator = true;
    }

    public void IndicateElektro()
    {
        elektroIndicator = true;
    }

    public void RemoveExplosion()
    {
        explosionIndicator = false;

        if (!elektroIndicator && !explosionIndicator)
            gameObject.renderer.material.color = explosionInactiveColor;
    }

    public void RemoveElektro()
    {
        elektroIndicator = false;

        if (!elektroIndicator && !explosionIndicator)
            gameObject.renderer.material.color = elektroInactiveColor;
    }
}
