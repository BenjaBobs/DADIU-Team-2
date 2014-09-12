using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	int health;
    public int posX;
    public int posY;
    public int occurenceFactor = 1;
    public int scoreValue = 1;

    bool holding = false;
    float startHoldTime;
    float holdTime = 1.5f;

	int Health 
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health < 1)
			{
				OnDeath();
			}
		}
	}

	// Use this for initialization
	void Start () {
        // Get Spawner
        Spawner spawner = transform.parent.GetComponent<Spawner>();
        posX = spawner.posX;
        posY = spawner.posY;

        Grid.InsertToGrid(posX, posY, gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    protected virtual void OnTap()
	{

	}

	protected virtual void OnHold()
	{

	}

	public virtual void OnDeath()
	{
        DestroyImmediate(gameObject);
        //TODO: Add score to score manager
	}

	void OnMouseDown()
	{
        startHoldTime = Time.time;
        holding = true;
		OnTap ();
	}

    void OnMouseUp()
    {
        holding = false;
    }

    void OnMouseOver()
    {
        if (holding)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if (Time.time - startHoldTime >= holdTime)
                {
                    holding = false;
                    OnHold();
                }
            }
        }
    }
}
