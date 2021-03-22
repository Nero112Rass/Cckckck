using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChasing : MonoBehaviour
{
    private Vector3 target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Camera").GetComponent<ScreenPointToRay>().groundLevelCursorPosition;

        target += new Vector3(0, 1, 0);

        transform.position = target;
    }
}
