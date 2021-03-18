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
    Vector3 forward, right, direction, rightMovement, upMovement; 
    public float turnTime = 0.1f;
    public float turnVelocity;   
    
    private bool isMoving;
    public Vector3 startPosition;
    public Vector3 destination;
    public Vector3 facingDirection;
    public float horizontal;
    public float vertical;
    public float E = new Vector3(0.01f, 0.01f, 0.01f).magnitude;
    public Vector3 distance;
    //LittleDash
    
    void Start()
    {
    	rb = GetComponent<Rigidbody>();
    	isMoving = false;
		startPosition = new Vector3(0, 4, 0);
		transform.position = startPosition;
		
    }

    void Update()
    {
    	horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");
    	if (!isMoving)
	    {
			//horizontal = Input.GetAxisRaw("Horizontal");
			//vertical = Input.GetAxisRaw("Vertical");
			
			//transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)); 
			if (horizontal != 0 || vertical  != 0)
			{
				facingDirection = Quaternion.Euler(0, 45, 0) * new Vector3(horizontal, 0f, vertical);
				startPosition = transform.position;
				destination = startPosition + facingDirection;
				isMoving = true;
				//rb.velocity = facingDirection * moveSpeed;
			}

			rb.MovePosition(new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)));
		}
		else if (isMoving && (destination - rb.position).magnitude < E)
		//else if (Mathf.Abs(distance.x) >= 1f || Mathf.Abs(distance.z) >= 1f)
		{
			isMoving = false;
			distance = Vector3.zero;
			//rb.velocity = Vector3.zero;
		}
		else if (isMoving)
		{
			rb.MovePosition(transform.position + facingDirection * moveSpeed * Time.deltaTime);
			distance += (transform.position + facingDirection * moveSpeed * Time.deltaTime);
		}
		
		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
		transform.rotation = Quaternion.Euler(0f, angle, 0f);
				
    }
}
