using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInput : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            anim.SetBool("Moving", true);
       else
            anim.SetBool("Moving", false);

        if (Input.GetMouseButton(0))
            anim.SetBool("Shooting0", true);
        else
            anim.SetBool("Shooting0", false);

        if (Input.GetMouseButton(1))
            anim.SetBool("Shooting1", true);
        else
            anim.SetBool("Shooting1", false);
    }
}
