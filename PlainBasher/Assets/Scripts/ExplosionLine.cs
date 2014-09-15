using UnityEngine;
using System.Collections;

public class ExplosionLine : MonoBehaviour
{
    public float lifetime;
    public float expandSpeed;
    [HideInInspector]
    public bool expandY;
    public int posX;
    public int posY;
    float timeLeft = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 scale = transform.localScale;
        if (expandY)
        {
            HitLine(posX, expandY);
        }
        else
        {
            HitLine(posY, expandY);
        }

        if (timeLeft <= 0)
            Destroy(gameObject);

        timeLeft -= Time.deltaTime;
    }

    void HitLine(int position, bool expandY)
    {
        int maxValue = (expandY ? Grid.GetMaxY() : Grid.GetMaxX());
        for (int i = 1; i <= maxValue; i++)
        {
            GameObject obj = (expandY ? Grid.LookupGrid(position, i) : Grid.LookupGrid(i, position));
            if (obj)
            {
                Mole mole = obj.GetComponent<Mole>();
                if (true)
                {
                    mole.OnDeath();
                }
            }
        }
    }
}
