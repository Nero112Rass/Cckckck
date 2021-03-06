using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Missles1 : MonoBehaviour
{



    Color DeltaColor;

    public Vector3 target;

    Renderer m_ObjectRenderer;

    public float step = 1;

    public float stepMove = 1;

    public ParticleSystem AuraH;
    public ParticleSystem AuraS;
    public bool sphereAura;


    public Transform playerChasing;



    public List<Missles1> MisslePrefabs1;


    public Vector3 placeChasing;

    public ScreenPointToRay ClickPosition;


    public bool fireMana;
    public bool waterMana;
    public bool earthMana;
    public bool aerMana;
    public bool darkMana;
    public bool lightMana;

    public bool empowered;

    public float cooldown;

    public bool drainer;

    public float materiaCost;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireCast == true && fireMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus == true)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus = false;
                empowered = true;
            }

        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterCast == true && waterMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus == true )
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus = false;
                empowered = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthCast == true && earthMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus == true)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus = false;
                empowered = true;
            }

        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerCast == true && aerMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus == true)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus = false;
                empowered = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkCast == true && darkMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus == true)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus = false;
                empowered = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightCast == true && lightMana == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightCast = false;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus == true)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus = false;
                empowered = true;
            }
        }





        if (!sphereAura)
            AuraS = AuraH;
        
        



        target = GameObject.Find("Camera").GetComponent<ScreenPointToRay>().clickPosition1;
        target += new Vector3(0, 1, 0);
        playerChasing = GameObject.Find ("Bip001 R Hand").GetComponent<Transform>();



        m_ObjectRenderer = GetComponent<Renderer>();

        DeltaColor = m_ObjectRenderer.material.color;
        DeltaColor.a = 0;
        m_ObjectRenderer.material.color = DeltaColor;



    }

    // Update is called once per frame
    void Update()
    {
        if (drainer)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireCast == true && fireMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus = false;
                    empowered = true;
                }

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterCast == true && waterMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus = false;
                    empowered = true;
                }
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthCast == true && earthMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus = false;
                    empowered = true;
                }

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerCast == true && aerMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus = false;
                    empowered = true;
                }
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkCast == true && darkMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus = false;
                    empowered = true;
                }
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightCast == true && lightMana == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightCast = false;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus == true)
                {

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus = false;
                    empowered = true;
                }
            }
        }



        placeChasing = playerChasing.position + new Vector3(1, 1, -1);

        if (Vector3.Distance(gameObject.transform.position, target) > 1)
        {
            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.a < 1)
                DeltaColor.a += step * Time.deltaTime;
            m_ObjectRenderer.material.color = DeltaColor;
            if (!AuraH.isPlaying)
            {
                AuraH.Play();
            }
            if (!AuraS.isPlaying)
            {
                AuraS.Play();
            }
        }

        if (empowered)
        {
            AuraH.transform.localScale = AuraH.transform.localScale * 3/2;
            //transform.localScale = transform.localScale * 2;//
            empowered = false;
        }




        if (DeltaColor.a >= 1)
        {
            if (Vector3.Distance(gameObject.transform.position, target) > 0)
            {

                transform.position -= (transform.position - target) * Time.deltaTime * 2;
            }
        }


        if (DeltaColor.a < 1)
        {
            if (Vector3.Distance(gameObject.transform.position, target) > 1)
                transform.position = Vector3.Lerp(transform.position, playerChasing.position + new Vector3 (-1/2,1,-1/2), 5*Time.deltaTime);
        }


        if (Vector3.Distance(gameObject.transform.position, target) <= 1)
        {
            
            DeltaColor = m_ObjectRenderer.material.color;
            if (DeltaColor.a >= 0)
            {
                DeltaColor.a -= step * Time.deltaTime * 15;
                m_ObjectRenderer.material.color = DeltaColor;

            }
            if (AuraH.isPlaying)
            {
                AuraH.Stop();
            }
            if (AuraS.isPlaying)
            {
                AuraS.Stop();
            }

        }
        if (Vector3.Distance(gameObject.transform.position, target) <= 1)
        {
            Destroy(gameObject);
        }
    }
}
