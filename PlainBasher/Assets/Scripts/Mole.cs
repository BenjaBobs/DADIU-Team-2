using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	int health = 1;
    [HideInInspector]
    public int posX;
    [HideInInspector]
    public int posY;
    public int occurenceFactor = 1;
    public int scoreValue = 1;
    public float popDistance = 5.0f;
    public float popSpeed = 0.5f;

    [HideInInspector]
    public float currentDistance = 0.0f;
    public float timeUp = 6.0f;
    public float lerpSpeed = 10.0f;

    bool holding = false;
    float startHoldTime;
    
    public float holdTime = 0.5f;

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
			OnHealthChange();
			if (health < 1)
			{
				OnDeath();
			}
		}
	}

	protected virtual void OnHealthChange()
	{

	}

	// Use this for initialization
	void Start () {        
	}

	public void UpdateGridPosition(int posx, int posy)
	{
		posX = posx;
		posY = posy;
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

	protected virtual void PlayDeathSound()
	{

	}

	public virtual void OnDeath()
	{
        Player.Score++;
		PlayDeathSound ();
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

    void OnMouseExit()
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
		float distance = (currentPopSpeed * lerpSpeed + popSpeed) * Settings.instance.GetDeltaTime();
        if (!movingDown)
        {
            distance = Mathf.Min(distance + currentDistance, popDistance) - currentDistance;
        }
        Vector3 currentPosition = transform.position;

        if (movingDown && currentTimeUp < timeUp)
        {
            distance = 0;
			currentTimeUp += Settings.instance.GetDeltaTime() * Settings.instance.GetDifficultyStayTime();
			if (currentTimeUp >= timeUp)
				OnFlee();
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
        else if (currentDistance <= 0)
        {
			Player.Lives--;
            Destroy(gameObject);
        }
    }

	protected virtual void OnFlee()
	{
		Animator anim = gameObject.GetComponentInChildren<Animator>();
		if (anim)
			anim.SetBool("Exiting", true);
	}
}
