using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Activatable activatable;

    private bool isActivatable = false;
    public Action Activate;
    public Action Desactivate;

    // Start is called before the first frame update
    void Start()
    {
        Activate += activatable.OnActivate;
        Activate += activatable.OnDesactivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivatable)
        {
            GetComponent<Renderer>().material.color = Color.green;
            activatable.isActivated = true;

            Activate?.Invoke();
        }
        else
        {
            
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pushable"))
        {
            isActivatable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pushable"))
        {
            activatable.isActivated = false;
            isActivatable = false;
            Desactivate?.Invoke();
        }
    }
}
