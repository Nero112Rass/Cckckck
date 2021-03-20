using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkening : MonoBehaviour
{
    public Transform player;

    public float step;

    public bool Vision;
    Color VanillaColor;
    Color DeltaColor;


    public bool start = false;

    Renderer m_ObjectRenderer;

    void Start()
    {
        step = 1;
        m_ObjectRenderer = GetComponent<Renderer>();
        VanillaColor = m_ObjectRenderer.material.color;
        DeltaColor = m_ObjectRenderer.material.color;


        Vision = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))

            Vision = !Vision;

        if (Vision)
        {

            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.r > 0)
                DeltaColor.r -= step * Time.deltaTime * 5;
            if (DeltaColor.g > 0)
                DeltaColor.g -= step * Time.deltaTime * 5;
            if (DeltaColor.b > 0)
                DeltaColor.b -= step * Time.deltaTime * 5;
            m_ObjectRenderer.material.color = DeltaColor;






        }
        if (!Vision)
        {

            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.r < VanillaColor.r)
                DeltaColor.r += step * Time.deltaTime * 5;
            if (DeltaColor.g < VanillaColor.g) 
                DeltaColor.g += step * Time.deltaTime * 5;
            if (DeltaColor.b < VanillaColor.b)
                DeltaColor.b += step * Time.deltaTime * 5;
            m_ObjectRenderer.material.color = DeltaColor;

        }

    }
}
