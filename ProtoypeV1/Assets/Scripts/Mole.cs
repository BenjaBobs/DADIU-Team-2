using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	GameObject player;
	public int minHealth = 1;
	public int maxHealth = 1;
	public float healthScaleMultiplier = 1.0f;
	int currentHealth;
	public int score;
	public Transform exploderY;
	public Transform exploderX;

    public GameObject iceLayer;

	public Transform exploderGrid;
	public bool allowChainReaction;
	public int occurenceFactor = 1;
	Vector3 originalColliderSize;

    //stuff for Hold mechanic
    bool holding = false;
    float startTime;
    float holdTime = 2;

	// Use this for initialization
	void Start () {
		originalColliderSize = transform.gameObject.GetComponent<BoxCollider> ().size;
		// Random color
		//gameObject.renderer.material.color = new Color (Random.value, Random.value, Random.value);
		player = GameObject.Find ("Player");
		currentHealth = Random.Range(minHealth, maxHealth + 1);
		UpdateScale ();
	}

	void OnMouseDown () {
        startTime = Time.time;
        holding = true;

		currentHealth--;
		UpdateScale ();
		if (currentHealth < 1) {
			//Die(true);		
		}
	}

    void OnMouseOver()
    {
        if (holding)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if (Time.time - startTime >= holdTime)
                {
                    holding = false;
                    Debug.Log("You've held");
                }
            }
        }
    }

	public void Die(bool effect = false)
	{
		if (gameObject.name.Contains("Exploder1Mole")) 
		{
			Transform explosionX = (Transform)Instantiate(exploderX, transform.position, transform.rotation);
			explosionX.gameObject.GetComponent<ExplosionLine>().expandY = false;
			Transform explosionY = (Transform)Instantiate(exploderY, transform.position, transform.rotation);
			explosionY.gameObject.GetComponent<ExplosionLine>().expandY = true;
		}
		else if (gameObject.name.Contains("Exploder2Mole")) 
		{
			Instantiate(exploderGrid, transform.position, transform.rotation);
		}
        else if (gameObject.name.Contains("FreezeMole"))
        {
            GameObject ice = Instantiate(iceLayer, new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
        }
		((Player)player.gameObject.GetComponent(typeof(Player))).IncreaseScore(score);
		Destroy (gameObject);
	}

	void UpdateScale()
	{
		float scale =  (1 + (currentHealth - 1) * healthScaleMultiplier);
		transform.localScale = new Vector3(1,1,1) * scale;
		Vector3 colliderSize = originalColliderSize;
		colliderSize /= scale;
		transform.gameObject.GetComponent<BoxCollider> ().size = colliderSize;
	}
}