using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class ScreenPointToRay : MonoBehaviour
{
    
    public Vector3 clickPosition0;
    public Vector3 clickPosition1;
    private void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition0 = hit.point;
            }


        }
        if (Input.GetMouseButtonDown(1))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition1 = hit.point;
            }

        }
    }
}
