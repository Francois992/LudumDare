using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour
{
    public bool isInsideTrigger = false;

    public TeleportationTrigger otherTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (otherTrigger.isInsideTrigger || isInsideTrigger)
            return;

        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector2(otherTrigger.transform.position.x, other.gameObject.transform.position.y);
            otherTrigger.isInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInsideTrigger = false;
        }
    }
}
