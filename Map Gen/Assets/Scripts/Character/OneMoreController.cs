using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OneMoreController : MonoBehaviour
{
    public NavMeshAgent agent;

    private Rigidbody rb;


    //public CharacterController controller;
    public Transform cam;

    //Move
    public float moveSpeed = 20f;
    public float turnTime = 0.1f;
    public float turnVelocity;

    public Vector3 direction;
    public Vector3 newDirection;
    //private 
    public bool isMoving;
    private bool hadTurned;
    public Vector3 startPosition;
    public Vector3 destination;
    public float horizontal;
    public float vertical;
    public float E = new Vector3(0.01f, 0.01f, 0.01f).magnitude;

    // Main
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        hadTurned = false;
        startPosition = new Vector3(0, 4, 0);
        transform.position = startPosition;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        newDirection = Quaternion.Euler(0, 45, 0) * new Vector3(horizontal, 0f, vertical);
        newDirection = new Vector3(Mathf.Round(newDirection.x), 0f, Mathf.Round(newDirection.z));
        /*
        if (horizontal != 0 || vertical != 0)
            agent.autoBraking = false;
        else
            agent.autoBraking = true;
        */
        // Initiate movement
        if (!isMoving)
        {
            if (horizontal != 0 || vertical != 0)
            {
                direction = newDirection;
                startPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
                destination = startPosition + direction;
                isMoving = true;
                agent.SetDestination(destination);
            }
        }
        // Turn
        else if (agent.hasPath)
        {
            if ((destination - transform.position).magnitude < E)
            {
                agent.SetDestination(destination + newDirection);
            }
            if (direction.magnitude > newDirection.magnitude && (newDirection.x != direction.x ^ newDirection.z != direction.z) && !hadTurned)
            {
                hadTurned = true;
                direction = newDirection;
                destination = startPosition + direction;
                agent.SetDestination(destination);
            }
        }
        // Finish moving
        else if (!agent.hasPath)
        {
            isMoving = false;
            hadTurned = false;
        }
    }
}
