using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activatable
{
    private Vector3 initialPos;
    [SerializeField] private Vector3 endPosition = Vector3.zero;

    [SerializeField] private float moveDuration = 2f;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnActivate()
    {
        elapsedTime += Time.deltaTime;

        float ratio = elapsedTime / moveDuration;

        transform.position = Vector3.Lerp(initialPos, initialPos + endPosition, ratio);
    }
}
