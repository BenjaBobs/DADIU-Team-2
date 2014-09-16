using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {
	int health = 1;
    [HideInInspector]
    public int posX;
    [HideInInspector]
    public int posY;
    public int occurenceFactor = 1;
    public int scoreValue = 10;
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
	protected bool isDead = false;
	public int damageToPlayer = 1;

    //Scrolling point stuff
    static GameObject textPrefab;
    bool textLoaded = false;

    void Awake()
    {
        if (!textLoaded)
        {
            textLoaded = true;
            textPrefab = Resources.Load<GameObject>("Prefabs/PointText");
        }
    }

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
	}
	
	// Update is called once per frame
	void Update () 
	{
        MoleMovement();
	}

	public bool IsDead()
	{
		return isDead;
	}

    protected virtual void OnTap()
	{

	}

	protected virtual void OnHold()
	{

	}

	protected virtual void OnRelease() {

	}

	protected virtual void PlayDeathSound()
	{

	}

	public virtual void OnDeath(bool give_bonus = true)
	{
		if (isDead)
		    return;

		isDead = true;
		if (give_bonus)
		{
			PlayDeathSound ();

			GameObject pointText = Instantiate (textPrefab) as GameObject;
            pointText.GetComponent<PointText>().scoreValue = scoreValue;
			GUIText gText = pointText.GetComponent<GUIText> ();

			Vector3 textLocation = Camera.main.WorldToScreenPoint (transform.position);
			textLocation.x /= Screen.width;
			textLocation.y /= Screen.height;
			textLocation.y += 0.1f;

			if (scoreValue > 10)
					gText.fontSize = 45;

			pointText.transform.localPosition = textLocation;
		}

        DestroyImmediate(gameObject);
        //TODO: Add score to score manager
	}

    public virtual void OnChain()
    {
        
    }

	void OnMouseDown()
	{
        if (!Settings.instance.GetPaused())
        {

            startHoldTime = Time.time;
            holding = true;
		    OnTap ();
        }
	}

    void OnMouseUp()
    {
        holding = false;
		OnRelease ();
    }

    void OnMouseExit()
    {
        holding = false;
		OnRelease ();
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
			isDead = true;
			if (damageToPlayer > 0)
				Player.Lives -= damageToPlayer;
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
