using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blyat : MonoBehaviour
{
    private Transform cursor;
    public Transform cam;
    [SerializeField] private LayerMask groundLayerMask;


    private Rigidbody rb;

    private bool isMoving;
    public float horizontal;
    public float vertical;
    public float timeToMove;
    public float timeToDash;
    public float turnTime;
    public float turnVelocity;
    public float elapsedTime;

    public Vector3 newDirection;
    public Vector3 setDirection;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public float turnSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cursor = GameObject.Find("CursorTarget").GetComponent<Transform>();
    }

    void Update()
    {

        float targetAngle = Mathf.Atan2((cursor.position - transform.position).x, (cursor.position - transform.position).z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        /* Nahuy
        if (!IsGrounded())
            rb.MovePosition(transform.position + Vector3.down * Time.deltaTime);
        */
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        newDirection = Quaternion.Euler(0, 45, 0) * new Vector3(horizontal, 0f, vertical);
        newDirection = new Vector3(Mathf.Round(newDirection.x), 0f, Mathf.Round(newDirection.z));

        if (Input.GetKey(KeyCode.LeftShift) && newDirection.magnitude > 0 && !isMoving)
            StartCoroutine(DashPlayer(newDirection));

        if (newDirection.magnitude > 0 && !isMoving)
            StartCoroutine(MovePlayer(newDirection));
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        startPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        targetPosition = startPosition + direction;

        while (elapsedTime < timeToMove)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);

        isMoving = false;
    }

    private IEnumerator DashPlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        startPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        targetPosition = startPosition + direction * 3;

        while (elapsedTime < timeToDash)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);

        isMoving = false;
    }

    /* E eto nahuy
    private bool IsGrounded()
    {
        bool raycastHit = Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), Vector3.down, transform.rotation, 1f, groundLayerMask);
        return raycastHit;
    }
    */
}
