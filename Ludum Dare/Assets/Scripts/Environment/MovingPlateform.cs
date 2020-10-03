using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlateform : Activatable
{
    private Vector3 initialPos;
    [SerializeField] private Transform secondPos = null;

    [SerializeField] private float moveDuration = 2f;

    private Vector3 startMovePos;
    private Vector3 endMovePos;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        startMovePos = initialPos;

        endMovePos = secondPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnActivate()
    {
        elapsedTime += Time.deltaTime;

        float ratio = elapsedTime / moveDuration;

        transform.position = Vector3.Lerp(startMovePos, endMovePos, ratio);

        if(transform.position == endMovePos)
        {
            elapsedTime = 0;
            startMovePos = endMovePos;
            if (endMovePos != initialPos) endMovePos = initialPos;
            else endMovePos = secondPos.position;
        }
    }
}
