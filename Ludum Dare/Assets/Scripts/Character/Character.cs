using DG.Tweening;
using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public static Character instance;
    public bool isFreezing = false;
    public bool isExiting = false;

    public Player rewiredPlayer;
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

    public GameObject frozenCorpsePrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private ParticleSystem walkParticle;

    //[Header("Gravity")]
    //public float gravity = 20f;
    //public float fallSpeedMax = 10f;
    //public float verticalSpeed = 0f;

    Vector3 initialGravity;

    private Vector3 OrientationFacing = Vector3.right;
    private Vector3 OrientationStartPush;
    private Vector3 ScaleStartPush;

    private bool stopPull = false;
    private bool isPushing = false;
    private Pushable pushedObject;

    [SerializeField] private Animator animator = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rewiredPlayer = ReInput.players.GetPlayer(0);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        normalSpeed = speed;
        initialGravity = Physics.gravity;
        Physics.gravity = new Vector3(initialGravity.x, -9.8f * 3, initialGravity.z);
    }

    private void Update()
    {
        if (!isExiting)
        {
            horizontalMove = rewiredPlayer.GetAxis("MoveHorizontal") * speed;
        }
        else
        {
            horizontalMove = 0;
        }

        if (!isExiting)
        {
            if (rewiredPlayer.GetButtonDown("Jump") && !jump)
            {
                jump = true;
            }
        }

        if (horizontalMove > 0)
        {
            OrientationFacing = Vector3.right;
            if (!isPushing) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            animator.SetBool("Idle", false);
            if (!isPushing) animator.SetBool("IsRunning", true);

            walkParticle.Play();
        }
        else if (horizontalMove < 0)
        {
            OrientationFacing = Vector3.left;
            if (!isPushing) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            animator.SetBool("Idle", false);
            if(!isPushing) animator.SetBool("IsRunning", true);
            walkParticle.Play();
        }
        else if (horizontalMove == 0)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("Idle", true);

            walkParticle.Stop();
        }

        if (!isExiting)
            if (!jump && !isPushing) CheckForPushable();


        if (!isExiting)
            if (isPushing)
            {
                CheckPull();
                animator.SetBool("Idle", false);

                if (!rewiredPlayer.GetButton("Interact"))
                {
                    isPushing = false;
                    speed = normalSpeed;
                    pushedObject = null;
                    animator.SetBool("Idle", true);
                    animator.SetBool("IsPush", false);
                    animator.SetBool("IsPull", false);
                }
            }

        if (rewiredPlayer.GetButtonDown("EndTimeLoop"))
        {
            if (isExiting)
                return;

            if (!isFreezing)
            {
                GameManager.instance.EndTimeLoop();
                isFreezing = true;
            }
        }

        if (rewiredPlayer.GetButtonDown("Interact"))
        {
            if (!isExiting && ExitDoor.instance.interactable)
            {
                GameManager.instance.FinishedLevel();
                isExiting = true;
            }
        }
        
        if (rewiredPlayer.GetButtonDown("ReloadLevel"))
        {
            if (isExiting)
                return;

            if (!isExiting)
            {
                GameManager.instance.ReloadLevel();
                isExiting = true;
            }
        }
    }

    void FixedUpdate()
    {
        Move(horizontalMove);
        if (!isPushing) Jump();
        jump = false;
        //UpdateGravity();
    }

    private void CheckPull()
    {
        RaycastHit hit;

        if (OrientationFacing == OrientationStartPush)
        {
            animator.SetBool("IsPush", true);
            animator.SetBool("IsPull", false);
        }
        else
        {
            animator.SetBool("IsPush", false);
            animator.SetBool("IsPull", true);
            if (Physics.Raycast(transform.position, OrientationFacing, out hit, .5f))
            {
                stopPull = true;
            }
        }

    }

    private void CheckForPushable()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, OrientationFacing, out hit, .5f, layerMask))
        {
            if (hit.transform.CompareTag("Pushable"))
            {
                if (rewiredPlayer.GetButton("Interact") && pushedObject == null)
                {
                    isPushing = true;
                    pushedObject = hit.transform.GetComponent<Pushable>();
                    OrientationStartPush = OrientationFacing;
                    ScaleStartPush = transform.localScale;
                    animator.SetBool("IsPush", true);
                    animator.SetBool("IsPull", false);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("Idle", false);
                }

            }

        }

    }

    public void RestartLoop(TweenCallback tweenCallback = null)
    {
        Instantiate(frozenCorpsePrefab, new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z), Quaternion.identity); ;
        transform.DOMove(spawnPosition.position, 0f)
            .OnComplete(tweenCallback);
    }

    private void Jump()
    {
        if (isGrounded && jump)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Idle", false);
            animator.SetBool("IsRunning", false);
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        RaycastHit hit;

        if (!jump)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .55f, layerMask))
            {
                Debug.Log("Did hit the ground");
                animator.SetBool("Jumping", false);
                animator.SetBool("Idle", true);
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
        rb.MovePosition(new Vector3(transform.position.x + (dirX * Time.fixedDeltaTime), transform.position.y, transform.position.z));

        if (isPushing)
        {
            speed = speedPush;

            RaycastHit hit;

            if (Physics.Raycast(pushedObject.transform.position, Vector3.up, out hit, 1.1f))
            {
                if (hit.transform.CompareTag("Pushable"))
                {
                    return;
                }
            }

            if (Physics.Raycast(pushedObject.transform.position, new Vector3(pushedObject.transform.position.x + horizontalMove, pushedObject.transform.position.y, pushedObject.transform.position.z), out hit, 1.1f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    if (!stopPull) pushedObject.transform.position = Vector3.MoveTowards(pushedObject.transform.position, new Vector3(pushedObject.transform.position.x + horizontalMove * Time.deltaTime, pushedObject.transform.position.y, pushedObject.transform.position.z), speed * Time.deltaTime);
                }
                else
                {

                }

            }
            else
            {
                pushedObject.transform.position = Vector3.MoveTowards(pushedObject.transform.position, new Vector3(pushedObject.transform.position.x + horizontalMove * Time.deltaTime, pushedObject.transform.position.y, pushedObject.transform.position.z), speed * Time.deltaTime);
            }

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
