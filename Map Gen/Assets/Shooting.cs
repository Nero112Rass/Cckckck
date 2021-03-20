using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Shooting : MonoBehaviour
{





    public List<Missles0> MisslePrefabs0;
    public List<Missles1> MisslePrefabs1;







    void Start()
    {


    }



    // Update is called once per frame
    void Update()
    {



        if (Input.GetMouseButton(0))
        {


                Instantiate(MisslePrefabs0[0], transform.position + new Vector3 (-1, -1, 1), Quaternion.identity);

        }

        if (Input.GetMouseButton(1))
        {


                Instantiate(MisslePrefabs1[0], transform.position + new Vector3(1, -1, -1), Quaternion.identity);

        }

    }





}
