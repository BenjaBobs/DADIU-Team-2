using UnityEngine;
using System.Collections;

public class IceLayer : MonoBehaviour
{

    public int Health = 3;
	public GameObject breakParticle;

	void Start()
	{
		AudioManager.PlayIceAppear();
		AudioManager.LowPassFilter(true);
	}

    void OnMouseDown()
    {
        if (Settings.instance.GetPaused()) return;

		AudioManager.PlayTapIce ();
        Health--;
        if (Health <= 0) {
			if (breakParticle)
				Instantiate(breakParticle, transform.position, transform.rotation); 
			AudioManager.LowPassFilter(false);
            DestroyImmediate(gameObject);
		}

        if (Health == 2)
            renderer.material.mainTextureOffset = new Vector2(1/3f,0);
        else if (Health == 1)
            renderer.material.mainTextureOffset = new Vector2(2/3f, 0);
        
    }

}