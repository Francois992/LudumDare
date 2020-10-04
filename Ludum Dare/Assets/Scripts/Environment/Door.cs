using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activatable
{
    private Vector3 initialPos;
    [SerializeField] private Vector3 endPosition = Vector3.zero;

    [SerializeField] private float moveDuration = 2f;

    [SerializeField] private TriggerZone triggerZone = null;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
        {
            transform.DOMove(initialPos, moveDuration);
            if(triggerZone.isTouching) transform.DOPause();
        }      
    }

    public override void OnActivate()
    {
        transform.DOMove(endPosition, moveDuration);
    }

    
}
