using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour
{
    bool isTeleport = false;
    static bool isInsideTrigger = false;

    public Transform otherTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isTeleport)
        {
            other.transform.position = new Vector2(otherTrigger.position.x, other.gameObject.transform.position.y);
            isTeleport = true;
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isTeleport)
        {
            isTeleport = false;
        }
    }
}
