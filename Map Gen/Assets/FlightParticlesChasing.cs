using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightParticlesChasing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }
}
