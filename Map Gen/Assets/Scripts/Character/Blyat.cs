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
    private bool isDashing;
    private bool dashIsCooling;
    private float horizontal;
    private float vertical;
    public float timeToMove = 0.5f;
    public float timeToDash = 0.1f;
    public float coolDashTime = 0.5f;
    public float extraDashes;
    public float extraDashesValue = 3;
    public float turnTime = 0.2f;
    private float turnVelocity;
    private float elapsedTime;

    public Vector3 newDirection;
    public Vector3 setDirection;
    public Vector3 startNode;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public float turnSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cursor = GameObject.Find("CursorTarget").GetComponent<Transform>();
        extraDashes = extraDashesValue;
    }

    void Update()
    {
        // Char Turning
        float targetAngle = Mathf.Atan2((cursor.position - transform.position).x, (cursor.position - transform.position).z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        /* Nahuy
        if (!IsGrounded())
            rb.MovePosition(transform.position + Vector3.down * Time.deltaTime);
        */

        // Read Direction
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        newDirection = Quaternion.Euler(0, 45, 0) * new Vector3(horizontal, 0f, vertical);
        newDirection = new Vector3(Mathf.Round(newDirection.x), 0f, Mathf.Round(newDirection.z));

        // Dash and Move
        if (Input.GetKey(KeyCode.LeftShift) && newDirection.magnitude > 0 && !isMoving && extraDashes > 0)
            StartCoroutine(DashPlayer());
        else if (newDirection.magnitude > 0 && !isMoving)
            StartCoroutine(MovePlayer());

        // Cool Dash
        if (extraDashes < extraDashesValue && !isDashing && !dashIsCooling)
            StartCoroutine(DashCooldown());
        else if (isDashing)
            StopCoroutine(DashCooldown());
        
    }

    private IEnumerator MovePlayer()
    {
        isMoving = true;
        float elapsedTime = 0;

        yield return new WaitForSeconds(0.01f); // Not good solution

        Vector3 direction = newDirection;

        startNode = new Vector3(Mathf.Round(transform.position.x), transform.position.y, 
            Mathf.Round(transform.position.z));
        targetPosition = startNode + direction;

        while (elapsedTime < timeToMove)
        {
            rb.MovePosition(Vector3.Lerp(startNode, targetPosition, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);

        isMoving = false;
    }

    private IEnumerator DashPlayer()
    {
        isMoving = true;
        isDashing = true;
        extraDashes--;
        float elapsedTime = 0;

        yield return new WaitForSeconds(0.01f); // Not good solution

        Vector3 direction = newDirection;

        startNode = new Vector3(Mathf.Round(transform.position.x), transform.position.y, 
            Mathf.Round(transform.position.z));
        targetPosition = startNode + direction * 3;

        while (elapsedTime < timeToDash)
        {
            rb.MovePosition(Vector3.Lerp(startNode, targetPosition, (elapsedTime / timeToDash)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
        setDirection = Vector3.zero;

        isMoving = false;
        isDashing = false;
    }

    private IEnumerator DashCooldown()
    {
        dashIsCooling = true;

        float elapsedTime = 0;

        while (elapsedTime < coolDashTime)
        { 
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        extraDashes++;

        setDirection = Vector3.zero;

        dashIsCooling = false;
    }
    /* E eto nahuy
    private bool IsGrounded()
    {
        bool raycastHit = Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), Vector3.down, transform.rotation, 1f, groundLayerMask);
        return raycastHit;
    }
    */
}
