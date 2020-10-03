using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Activatable activatable;

    private bool isActivatable = false;
    public Action Activate;

    // Start is called before the first frame update
    void Start()
    {
        Activate += activatable.OnActivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivatable && Input.GetKey(KeyCode.E))
        {
            Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivatable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivatable = false;
        }
    }
}
