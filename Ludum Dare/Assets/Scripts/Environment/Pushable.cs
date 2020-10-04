using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 0f;
    [SerializeField] private float fallSpeedMax = 10f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private bool isGrounded;

    [SerializeField] public LayerMask detectionLayer = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        UpdateGravity();

        UpdatePos();
    }

    private void UpdateGravity()
    {
        if (isGrounded) return;

        verticalSpeed -= gravity * Time.deltaTime;
        if (verticalSpeed < -fallSpeedMax)
        {
            verticalSpeed = -fallSpeedMax;
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;
        //if (Physics.BoxCast(transform.position, Vector3.one, Vector3.down, out hit, Quaternion.identity, 1.1f, detectionLayer))
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.76f, detectionLayer))
        {
            isGrounded = true;
            verticalSpeed = 0f;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void UpdatePos()
    {
        Vector3 newPos = transform.position;
        newPos.y += verticalSpeed * Time.deltaTime;
        transform.position = newPos;
    }
}
