using UnityEngine;
using System.Collections;

public class ExplosionLine : MonoBehaviour
{
    public float lifetime;
    public float expandSpeed;
    float currentLifetime = 0.0f;
    [HideInInspector]
    public bool expandY;
    public int posX;
    public int posY;

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
            scale.z += Settings.instance.GetDeltaTime() * expandSpeed / lifetime;
            HitLine(posX, expandY);
        }
        else
        {
			scale.x += Settings.instance.GetDeltaTime() * expandSpeed / lifetime;
            HitLine(posY, expandY);
        }

        transform.localScale = scale;
		currentLifetime += Settings.instance.GetDeltaTime();

        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
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
