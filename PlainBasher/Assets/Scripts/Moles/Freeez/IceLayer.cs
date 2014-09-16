using UnityEngine;
using System.Collections;

public class IceLayer : MonoBehaviour
{

    public int Health = 3;

	void Start()
	{
		AudioManager.PlayIceAppear();
	}

    void OnMouseDown()
    {
		AudioManager.PlayTapIce ();
        Health--;
        if (Health <= 0)
            Destroy(gameObject);

        if (Health == 2)
            renderer.material.mainTextureOffset = new Vector2(1/3f,0);
        else if (Health == 1)
            renderer.material.mainTextureOffset = new Vector2(2/3f, 0);
        
    }

}