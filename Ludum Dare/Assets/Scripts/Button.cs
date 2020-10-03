using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Activatable activatable;

    public Action Activate;

    // Start is called before the first frame update
    void Start()
    {
        Activate += activatable.OnActivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Activate();
        }
    }
}
