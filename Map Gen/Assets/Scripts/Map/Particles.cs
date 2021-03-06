using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem Aura;
    public ParticleSystem AuraX2;

    public bool doubleAura;

    public Transform player;

    public float distance;

    public bool Vision;

    public bool fireAura;
    public bool waterAura;
    public bool earthAura;
    public bool aerAura;
    public bool darkAura;
    public bool lightAura;

    private bool doubleCharge;

    void Start()
    {
        doubleCharge = true;
        distance = 3;
        Vision = false;
        if (!doubleAura)
        {
            AuraX2 = Aura;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))

            Vision = !Vision;


        if (Vision)
        {

            
            if (doubleAura)
                if(doubleCharge)
                    if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
                    {
                        
                        Aura.Play();
                    }
                if (!doubleCharge&& !AuraX2.isPlaying)
                    if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
                    {
                        
                        AuraX2.Play();
                    }


            if (!Aura.isPlaying)
            {
                if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
                {
                    if (doubleAura)
                        if (doubleCharge)
                            Aura.Play();
                    if (doubleAura)
                        if (!doubleCharge && !AuraX2.isPlaying)
                            AuraX2.Play();
                    if (!doubleAura)
                        Aura.Play();

                }
                
            }


            if (Aura.isPlaying)
                if (Vector3.Distance(gameObject.transform.position, player.position) > distance)
                    Aura.Stop();
            if (doubleAura)
                if (AuraX2.isPlaying)
                    if (Vector3.Distance(gameObject.transform.position, player.position) > distance)
                        AuraX2.Stop();

        }
        if (!Vision)
            Aura.Stop();
        if (doubleAura)
            if (!Vision)
                AuraX2.Stop();

        if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireCast == true && fireAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus !=true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().fireBonus = true;
                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterCast == true && waterAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus != true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().waterBonus = true;

                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);


            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthCast == true && earthAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus != true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().earthBonus = true;
                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerCast == true && aerAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus != true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().aerBonus = true;
                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);


            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkCast == true && darkAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus != true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().darkBonus = true;
                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightCast == true && lightAura == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus != true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().lightBonus = true;
                if (doubleAura)
                    if (!doubleCharge)
                        Destroy(gameObject);
                if (doubleCharge)
                {
                    Aura.Stop();
                    AuraX2.Play();
                    doubleCharge = false;
                }

                if (!doubleAura)
                    Destroy(gameObject);
            }



        }
            

        

    }
}
