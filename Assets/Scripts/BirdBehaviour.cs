using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity;
    public float xVel;
    public float destroyTime;
    public float counter;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(xVel, rb.velocity.y);
    }

    void Update()
    {
        rb.MovePosition(rb.position + velocity);
        if (counter < destroyTime)
        {
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0;
            Destroy(gameObject);
        }
    }
}
