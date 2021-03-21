using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class ScreenPointToRay : MonoBehaviour
{
    
    public Vector3 clickPosition0;
    public Vector3 clickPosition1;

    public Vector3 cursorPosition;

    public Vector3 groundLevelCursorPosition;

    public LayerMask groundLayer;

    private void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition0 = hit.point;
            }


        }
        if (Input.GetMouseButton(1))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition1 = hit.point;
            }

        }

        Ray rayC = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitC;

        if (Physics.Raycast(rayC, out hitC))
        {
            cursorPosition = hitC.point;

        }


        Ray rayG = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitG;
        if (Physics.Raycast(rayG, out hitG, 100f, groundLayer))
        {
            groundLevelCursorPosition = hitG.point;

        }


    }
}
