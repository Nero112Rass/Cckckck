using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Shooting : MonoBehaviour
{





    public List<Missles0> MisslePrefabs0;
    public List<Missles1> MisslePrefabs1;


    public bool fireCast;
    public bool waterCast;
    public bool earthCast;
    public bool aerCast;
    public bool darkCast;
    public bool lightCast;


    public bool fireBonus;
    public bool waterBonus;
    public bool earthBonus;
    public bool aerBonus;
    public bool darkBonus;
    public bool lightBonus;



    private bool shootIsRecoiling00;
    public float shootRecoilTime00;
    private float shootRecoilTimeLeft00;
    private int extraShots00;
    public int extraShotsValue00 = 1;

    private bool shootIsRecoiling10;
    public float shootRecoilTime10;
    private float shootRecoilTimeLeft10;
    private int extraShots10;
    public int extraShotsValue10 = 1;

    private bool shootIsRecoiling01;
    public float shootRecoilTime01;
    private float shootRecoilTimeLeft01;
    private int extraShots01;
    public int extraShotsValue01 = 1;

    private bool shootIsRecoiling11;
    public float shootRecoilTime11;
    private float shootRecoilTimeLeft11;
    private int extraShots11;
    public int extraShotsValue11 = 1;



    


    void Start()
    {
        shootRecoilTime00 = MisslePrefabs0[0].cooldown;

        shootRecoilTime01 = MisslePrefabs0[1].cooldown;

        shootRecoilTime10 = MisslePrefabs1[0].cooldown;

        shootRecoilTime11 = MisslePrefabs1[1].cooldown;

        shootIsRecoiling00 = false;
        extraShots00 = extraShotsValue00;
        shootRecoilTimeLeft00 = shootRecoilTime00;

        shootIsRecoiling01 = false;
        extraShots01 = extraShotsValue01;
        shootRecoilTimeLeft01 = shootRecoilTime01;

        shootIsRecoiling10 = false;
        extraShots10 = extraShotsValue10;
        shootRecoilTimeLeft10 = shootRecoilTime10;

        shootIsRecoiling11 = false;
        extraShots11 = extraShotsValue11;
        shootRecoilTimeLeft11 = shootRecoilTime11;

    }



    // Update is called once per frame
    void Update()
    {

        if (extraShots00>0)
        {
            extraShots00--;

            shootIsRecoiling00 = true;
            if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.C))
            {
                if (MisslePrefabs0[0].fireMana)
                    fireCast = true;
                if (MisslePrefabs0[0].waterMana)
                    waterCast = true;
                if (MisslePrefabs0[0].earthMana)
                    earthCast = true;
                if (MisslePrefabs0[0].aerMana)
                    aerCast = true;
                if (MisslePrefabs0[0].darkMana)
                    darkCast = true;
                if (MisslePrefabs0[0].lightMana)
                    lightCast = true;




                Instantiate(MisslePrefabs0[0], transform.position + new Vector3(-1 / 2, -1, 1 / 2), Quaternion.identity);





            }

            
            

            
        }

        if (extraShots01 > 0)
        {
            extraShots01--;

            shootIsRecoiling01 = true;
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.C))
            {
                if (MisslePrefabs0[1].fireMana)
                    fireCast = true;
                if (MisslePrefabs0[1].waterMana)
                    waterCast = true;
                if (MisslePrefabs0[1].earthMana)
                    earthCast = true;
                if (MisslePrefabs0[1].aerMana)
                    aerCast = true;
                if (MisslePrefabs0[1].darkMana)
                    darkCast = true;
                if (MisslePrefabs0[1].lightMana)
                    lightCast = true;


                Instantiate(MisslePrefabs0[1], transform.position + new Vector3(-1 / 2, -1, 1 / 2), Quaternion.identity);


            }

            

        }
        if (extraShots10 > 0)
        {
            extraShots10--;

            shootIsRecoiling10 = true;
            if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.C))
            {
                if (MisslePrefabs1[0].fireMana)
                    fireCast = true;
                if (MisslePrefabs1[0].waterMana)
                    waterCast = true;
                if (MisslePrefabs1[0].earthMana)
                    earthCast = true;
                if (MisslePrefabs1[0].aerMana)
                    aerCast = true;
                if (MisslePrefabs1[0].darkMana)
                    darkCast = true;
                if (MisslePrefabs1[0].lightMana)
                    lightCast = true;

                Instantiate(MisslePrefabs1[0], transform.position + new Vector3(1 / 2, -1, -1 / 2), Quaternion.identity);

            }


        }

        if (extraShots11>0)
        {
            extraShots11--;

            shootIsRecoiling11 = true;

            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.C))
            {
                if (MisslePrefabs1[1].fireMana)
                    fireCast = true;
                if (MisslePrefabs1[1].waterMana)
                    waterCast = true;
                if (MisslePrefabs1[1].earthMana)
                    earthCast = true;
                if (MisslePrefabs1[1].aerMana)
                    aerCast = true;
                if (MisslePrefabs1[1].darkMana)
                    darkCast = true;
                if (MisslePrefabs1[1].lightMana)
                    lightCast = true;

                Instantiate(MisslePrefabs1[1], transform.position + new Vector3(1 / 2, -1, -1 / 2), Quaternion.identity);

            }

        }

        if (shootIsRecoiling00)
        {
            if (shootRecoilTimeLeft00 > 0)
            {
                shootRecoilTimeLeft00 -= Time.deltaTime;
            }
            else
            {
                shootRecoilTimeLeft00 = shootRecoilTime00;
                extraShots00++;
                if (extraShots00 == extraShotsValue00)
                {
                    shootIsRecoiling00 = false;
                }
            }
        }


        if (shootIsRecoiling01)
        {
            if (shootRecoilTimeLeft01 > 0)
            {
                shootRecoilTimeLeft01 -= Time.deltaTime;
            }
            else
            {
                shootRecoilTimeLeft01 = shootRecoilTime01;
                extraShots01++;
                if (extraShots01 == extraShotsValue01)
                {
                    shootIsRecoiling01 = false;
                }
            }
        }
        if (shootIsRecoiling10)
        {
            if (shootRecoilTimeLeft10 > 0)
            {
                shootRecoilTimeLeft10 -= Time.deltaTime;
            }
            else
            {
                shootRecoilTimeLeft10 = shootRecoilTime10;
                extraShots10++;
                if (extraShots10 == extraShotsValue10)
                {
                    shootIsRecoiling10 = false;
                }
            }
        }
        if (shootIsRecoiling11)
        {
            if (shootRecoilTimeLeft11 > 0)
            {
                shootRecoilTimeLeft11 -= Time.deltaTime;
            }
            else
            {
                shootRecoilTimeLeft11 = shootRecoilTime11;
                extraShots11++;
                if (extraShots11 == extraShotsValue11)
                {
                    shootIsRecoiling11 = false;
                }
            }
        }


    }





}
