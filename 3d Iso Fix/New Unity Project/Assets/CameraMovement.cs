using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform CameraTarget;

    public float smoothSpeed = 100f;
    public Vector3 offset;
    [SerializeField]
    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        CameraTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraTarget.position + offset, smoothSpeed * Time.deltaTime * 25);
    }
}