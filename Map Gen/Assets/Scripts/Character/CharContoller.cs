using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharContoller : MonoBehaviour
{


    [SerializeField]

    private Rigidbody rb;

    //Move
    public float movespeed = 10f;
    Vector3 forward, right;

    
    //Dash
    public Vector3 dashDirection;
    private bool dashIsRecoiling;
    private bool isDashing;
    public float dashSpeed;
    private float dashTimeLeft;
    public float dashTime;
    public float dashRecoilTime;
    private float dashRecoilTimeLeft;
    public float holdDashMultiplayer = 1f;
    public float normalDashMultiplayer = 1f;
    private int extraDashes;
    public int extraDashesValue = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isDashing = false;
        dashIsRecoiling = false;
        extraDashes = extraDashesValue;
        dashRecoilTimeLeft = dashRecoilTime;

      

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;




    }

    void Update()
    {
        //Move

        if (Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                Move();

            //Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && extraDashes > 0)
        {
            extraDashes--;  
            isDashing = true;
            dashIsRecoiling = true;

            Vector3 DrightMovement = right * movespeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
            Vector3 DupMovement = forward * movespeed * Time.deltaTime * Input.GetAxis("VerticalKey");

            Vector3 Dheading = Vector3.Normalize(DrightMovement + DupMovement);

            dashDirection = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
            rb.velocity = dashSpeed * Dheading;

        }
        else if (dashTimeLeft <= 0)
        {
            dashTimeLeft = dashTime;
            rb.velocity = new Vector3(0, 0);
            isDashing = false;
        }
        else if (isDashing && dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
                //rb.velocity = dashSpeed * dashDirection;  //!!!

        }

        //Dash Recoil
        if (dashIsRecoiling)
        {
            if (dashRecoilTimeLeft > 0)
            {
                    dashRecoilTimeLeft -= Time.deltaTime;
            }
            else
            {
                dashRecoilTimeLeft = dashRecoilTime;
                extraDashes++;
                if (extraDashes == extraDashesValue)
                {
                    dashIsRecoiling = false;
                }
            }
        }




        void Move()
        {
            Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
            Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
            Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxis("VerticalKey");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }


    }

    
}

