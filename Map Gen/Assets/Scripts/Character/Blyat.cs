using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blyat : MonoBehaviour
{
    private Rigidbody rb;

    private bool isMoving;
    public float horizontal;
    public float vertical;
    public float timeToMove;
    public float elapsedTime;
    public Vector3 newDirection;
    public Vector3 setDirection;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    

    void Start()
    {
    	rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /* не робит без IsGrounded
        if (!IsGrounded())
            rb.MovePosition(transform.position + Vector3.down * Time.deltaTime);
        */
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        newDirection = Quaternion.Euler(0, 45, 0) * new Vector3(horizontal, 0f, vertical);
        newDirection = new Vector3(Mathf.Round(newDirection.x), 0f, Mathf.Round(newDirection.z));
        if (newDirection.magnitude > 0 && !isMoving)
        {
            setDirection = newDirection;
            StartCoroutine(MovePlayer(setDirection));
        }
     }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        startPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        targetPosition = startPosition + direction;

        while(elapsedTime < timeToMove)
        {
            //transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / timeToMove));
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //transform.position = targetPosition;
        rb.MovePosition(targetPosition);

        isMoving = false;
    }
    /* сюда надо вписать слой
    private bool IsGrounded()
    {
        RaycastHit raycastHit = Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), Vector3.down, Quaternion.identity, 1f);

        return raycastHit.collider != null;
    }
    */
}
