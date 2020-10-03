using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    Player rewiredPlayer;
    Rigidbody rb;
    float horizontalMove = 0f;
    public bool isGrounded = true;
    public bool jump = false;

    public float jumpForce = 400f;
    public float speed = 10.0f;
    private float normalSpeed = 10.0f;

    public float speedPush = 5.0f;
    public float fallSpeed = 10.0f;
    public LayerMask layerMask;

    //[Header("Gravity")]
    //public float gravity = 20f;
    //public float fallSpeedMax = 10f;
    //public float verticalSpeed = 0f;

    Vector3 initialGravity;

    private Vector3 OrientationFacing = Vector3.right;

    private bool isPushing = false;
    private Pushable pushedObject;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    private void Start()
    {
        normalSpeed = speed;
        initialGravity = Physics.gravity;
        Physics.gravity = new Vector3(initialGravity.x, Physics.gravity.y * 3, initialGravity.z);
    }

    private void Update()
    {
        horizontalMove = rewiredPlayer.GetAxis("MoveHorizontal") * speed;

        if (rewiredPlayer.GetButtonDown("Jump") && !jump)
        {
            jump = true;
        }

        if (horizontalMove > 0) OrientationFacing = Vector3.right;
        else if (horizontalMove < 0) OrientationFacing = Vector3.left;

        CheckForPushable();

        if (isPushing)
        {
            if (!Input.GetKey(KeyCode.E))
            {
                isPushing = false;
                speed = normalSpeed;
                pushedObject = null;
            }
        }
    }

    void FixedUpdate()
    {
        Move(horizontalMove);
        Jump();
        jump = false;
        //UpdateGravity();
    }

    private void CheckForPushable()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, OrientationFacing, out hit, .5f))
        {
            if (hit.transform.CompareTag("Pushable"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    isPushing = true;
                    pushedObject = hit.transform.GetComponent<Pushable>();
                }
            }
        }
       
    }

    private void Jump()
    {
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        RaycastHit hit;

        if (!jump)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, layerMask))
            {
                Debug.Log("Did hit the ground");
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

        if (isPushing)
        {
            speed = speedPush;
            pushedObject.transform.position = Vector3.MoveTowards(pushedObject.transform.position, new Vector2(pushedObject.transform.position.x + horizontalMove, pushedObject.transform.position.y), speed *Time.deltaTime);
        }
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
