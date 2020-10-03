using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    public bool isActivated = false;

    virtual public void OnActivate()
    {
        Debug.Log("je suis actif");
    }
    
    virtual public void OnDesactivate()
    {
        Debug.Log("je suis actif");
    }
}
