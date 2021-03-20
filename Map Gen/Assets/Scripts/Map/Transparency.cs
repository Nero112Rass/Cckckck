using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{

    public Transform player;

    public float distance;
    public float step;

    public bool Vision;

    Color DeltaColor;


    public bool start = false;

    Renderer m_ObjectRenderer;

    

    void Start()
    {
        step = 1;
        distance = 4;
        m_ObjectRenderer = GetComponent<Renderer>();



        DeltaColor = m_ObjectRenderer.material.color;
        DeltaColor.a = 0;
        m_ObjectRenderer.material.color = DeltaColor;
        Vision = false;

    }


    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.V))
               
                Vision = !Vision;


        if (Vision)
            {

            {
                if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
                {
                    start = !start;
                    DeltaColor = m_ObjectRenderer.material.color;
                    if (DeltaColor.a < 1)
                        DeltaColor.a += step * Time.deltaTime*5;
                    m_ObjectRenderer.material.color = DeltaColor;
                }
                else
                {
                    start = false;
                }


            }
        }
        if (!Vision)
            start = false;
        
        if (!start)
        {
            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.a > 0)
                DeltaColor.a -= step*Time.deltaTime;
                m_ObjectRenderer.material.color = DeltaColor;
        }



            

        
    }
}
