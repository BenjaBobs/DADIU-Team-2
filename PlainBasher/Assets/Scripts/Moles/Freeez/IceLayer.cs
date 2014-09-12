using UnityEngine;
using System.Collections;

public class IceLayer : MonoBehaviour
{

    public int Health = 10;

    void OnMouseDown()
    {
        Health--;
        if (Health <= 0)
            Destroy(gameObject);

        Color clr = renderer.material.color;
        clr.a -= 0.05f;
        renderer.material.color = clr;
    }

}