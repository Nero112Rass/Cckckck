using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

	private Rigidbody rb;
	
	//public CharacterController controller;
	public Transform cam;
	
	//Move
    public float moveSpeed = 20f;
    public float turnTime = 0.1f;
    public float turnVelocity;   
    
    public Vector3 direction;
    public Vector3 newDirection;
    private bool isMoving;
    private bool hadTurned;
    public Vector3 startPosition;
    public Vector3 destination;
    public float horizontal;
    public float vertical;
    public float E = new Vector3(0.01f, 0.01f, 0.01f).magnitude;
    public Vector3 distance;
    //LittleDash
    
    void Start()
    {
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
    	if (!isMoving)
	    {
			//rb.MovePosition(new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)));
			if (horizontal != 0 || vertical  != 0)
			{
				direction = newDirection;
				startPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
				destination = startPosition + direction;
				isMoving = true;
			}
			
		}
		
		else if (isMoving && (destination - rb.position).magnitude < E)
		{
				isMoving = false;
				hadTurned = false;
		}
		else if (isMoving)
		{
			if (direction.magnitude < newDirection.magnitude && (newDirection.x != direction.x ^ newDirection.z != direction.z) && !hadTurned)
			{
				hadTurned = true;
				direction = newDirection;
				destination = startPosition + direction;
			}
			rb.MovePosition(transform.position + (destination - transform.position).normalized * moveSpeed * Time.deltaTime);
		}
		
		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
		transform.rotation = Quaternion.Euler(0f, angle, 0f);
				
    }
}
