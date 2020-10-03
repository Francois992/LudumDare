using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Player player;
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
        if (isActivatable && player.GetButton("Interact"))
        {
            Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<Character>().rewiredPlayer;
            isActivatable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            isActivatable = false;
        }
    }
}
