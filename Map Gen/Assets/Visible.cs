using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour
{
    MeshRenderer sprite;
    void Start()
    {
        sprite = GetComponent<MeshRenderer>();

    }

    IEnumerator VisibleSprite()
    {
        for (float f = 1f; f >= 1; f+= 0.05f)
        {
            Color color = sprite.material.color;
            color.a = f;
            sprite.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartVisible()
    {
        StartCoroutine("VisibleSprite");
    }

    void Update()
    {
        
    }
}
