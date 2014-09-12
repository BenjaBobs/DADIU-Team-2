using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	int health = 1;
    public int posX;
    public int posY;
    public int occurenceFactor = 1;
    public int scoreValue = 1;
    public float popDistance = 5.0f;
    public float popSpeed = 0.1f;
    public float currentDistance = 0.0f;
    public float timeUp = 3.0f;
    public float lerpSpeed = 10.0f;

    bool holding = false;
    float startHoldTime;
    float holdTime = 1.5f;
    float currentTimeUp = 0.0f;
    bool movingDown = false;

	public int Health 
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
        MoleMovement();
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

    void MoleMovement()
    {
        float currentPopSpeed = (currentDistance / popDistance);
        if (!movingDown)
            currentPopSpeed = 1 - currentPopSpeed;
        float distance = (currentPopSpeed * lerpSpeed + popSpeed) * Time.deltaTime * Settings.instance.GetDifficultySpeed();
        if (!movingDown)
        {
            distance = Mathf.Min(distance + currentDistance, popDistance) - currentDistance;
        }
        Vector3 currentPosition = transform.position;

        if (movingDown && currentTimeUp < timeUp)
        {
            distance = 0;
            currentTimeUp += Time.deltaTime * Settings.instance.GetDifficultyStayTime();
        }

        if (movingDown)
        {
            distance *= -1;
        }
        currentPosition.y += distance;
        transform.position = currentPosition;

        currentDistance += distance;

        if (currentDistance >= popDistance)
        {
            movingDown = true;
        }
        else if (currentDistance < 0)
        {
            Destroy(gameObject);
        }
    }
}
