using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 0.02f;
    public float deadZone = -45;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        p += moveSpeed * Vector3.left * Time.deltaTime;
        transform.position = p;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}