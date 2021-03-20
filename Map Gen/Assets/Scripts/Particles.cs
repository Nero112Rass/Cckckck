using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem Aura;

    public Transform player;

    public float distance;

    public bool Vision;


    void Start()
    {
        distance = 4;
        Vision = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))

            Vision = !Vision;


        if (Vision)
        {
            if (!Aura.isPlaying)
            {
                if (Vector3.Distance(gameObject.transform.position, player.position) <= distance)
                {
                    Aura.Play();
                }
                
            }

            if (Aura.isPlaying)
                if (Vector3.Distance(gameObject.transform.position, player.position) > distance)
                    Aura.Stop();
        }
        if (!Vision)
            Aura.Stop();
        
    }
}
