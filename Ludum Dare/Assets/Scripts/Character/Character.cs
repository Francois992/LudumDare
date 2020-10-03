using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    Rigidbody rb;
    float horizontalMove = 0f;
    public bool isGrounded = true;
    public bool jump = false;

    public float jumpForce = 400f;
    public float speed = 10.0f;
    public LayerMask layerMask;

    public bool ismoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if (horizontalMove != 0 && !ismoving)
            ismoving = true;
        else if(horizontalMove == 0)
            ismoving = false;
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        Move(horizontalMove);
        Jump();
        jump = false;
    }

    private void Jump()
    {
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        RaycastHit hit;

        if (!isGrounded && !jump)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, layerMask))
            {
                Debug.Log("Did hit the ground");
                isGrounded = true;
            }
        }
    }

    void Move(float dirX)
    {
        rb.MovePosition(new Vector2(transform.position.x + (dirX * Time.fixedDeltaTime), transform.position.y));
    }
}
