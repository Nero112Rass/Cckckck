using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missles0 : MonoBehaviour
{    Color DeltaColor;

    public Vector3 target;

    public bool start = false;

    Renderer m_ObjectRenderer;

    public float step = 1;

    public float stepMove = 1;

    public ParticleSystem Aura;

    public Transform playerChasing;




    public Vector3 placeChasing;

    public ScreenPointToRay ClickPosition;
    void Start()
    {
        target = GameObject.Find("Camera").GetComponent<ScreenPointToRay>().clickPosition0;
        target += new Vector3(0, 1, 0);
        playerChasing = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        

        m_ObjectRenderer = GetComponent<Renderer>();

        DeltaColor = m_ObjectRenderer.material.color;
        DeltaColor.a = 0;
        m_ObjectRenderer.material.color = DeltaColor;



    }

    // Update is called once per frame
    void Update()
    {

        placeChasing = playerChasing.position + new Vector3(-1, 1, 1);

        if (Vector3.Distance(gameObject.transform.position, target) > 1)
        {
            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.a < 1)
                DeltaColor.a += step * Time.deltaTime;
            m_ObjectRenderer.material.color = DeltaColor;
            if (!Aura.isPlaying)
            {
                Aura.Play();
            }
        }





        if (DeltaColor.a >= 1)
        {
            if (Vector3.Distance(gameObject.transform.position, target) > 0)
                transform.position -= (transform.position - target) * Time.deltaTime*2;
        }


        if (DeltaColor.a < 1)
        {
            if (Vector3.Distance(gameObject.transform.position, target) > 1)
                transform.position = Vector3.Lerp(transform.position, playerChasing.position + new Vector3 (-1,1,1), 5*Time.deltaTime);
        }


        if (Vector3.Distance(gameObject.transform.position, target) <= 1)
        {
            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.a >= 0)
            {
                DeltaColor.a -= step * Time.deltaTime * 15;
                m_ObjectRenderer.material.color = DeltaColor;

            }
            if (Aura.isPlaying)
            {
                Aura.Stop();
            }
            
        }
        
    }

    
}
