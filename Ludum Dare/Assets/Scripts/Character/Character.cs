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
    public float fallSpeed = 10.0f;
    public LayerMask layerMask;

    //[Header("Gravity")]
    //public float gravity = 20f;
    //public float fallSpeedMax = 10f;
    //public float verticalSpeed = 0f;

    Vector3 initialGravity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        initialGravity = Physics.gravity;
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

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
        //UpdateGravity();
    }

    private void Jump()
    {
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce);
            Physics.gravity = new Vector3(initialGravity.x, Physics.gravity.y *2, initialGravity.z);
            isGrounded = false;
        }

        RaycastHit hit;

        if (!jump)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, layerMask))
            {
                Debug.Log("Did hit the ground");
                Physics.gravity = initialGravity;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }

    void Move(float dirX)
    {
        rb.MovePosition(new Vector2(transform.position.x + (dirX * Time.fixedDeltaTime), transform.position.y));
        //rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
    }

    //void UpdateGravity()
    //{
    //    if (isGrounded)
    //        return;

    //    verticalSpeed -= gravity * Time.fixedDeltaTime;
    //    if(verticalSpeed < -fallSpeedMax)
    //    {
    //        verticalSpeed = -fallSpeedMax;
    //    }
    //}
}
