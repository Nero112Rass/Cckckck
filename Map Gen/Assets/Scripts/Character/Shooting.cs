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



        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.C))
        {


                Instantiate(MisslePrefabs0[0], transform.position + new Vector3 (-1/2, -1, 1/2), Quaternion.identity);

        }

        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.C))
        {


                Instantiate(MisslePrefabs1[0], transform.position + new Vector3(1/2, -1, -1/2), Quaternion.identity);

        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.C))
        {


            Instantiate(MisslePrefabs0[1], transform.position + new Vector3(-1/2, -1, 1/2), Quaternion.identity);

        }

        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.C))
        {


            Instantiate(MisslePrefabs1[1], transform.position + new Vector3(1/2, -1, -1/2), Quaternion.identity);

        }


    }





}
