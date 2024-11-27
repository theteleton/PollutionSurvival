using UnityEngine;
using System;
public class main : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    float vertical, horizontal;
    public Vector2 vector2 = new Vector2(-10, 10);

    Rigidbody2D rg2d;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rg2d.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }
}