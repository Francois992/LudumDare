using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour
{
    public bool isInsideTrigger = false;
    public bool isPushableInsideTrigger = false;

    public TeleportationTrigger otherTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (otherTrigger.isInsideTrigger || isInsideTrigger)
            return;

        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(otherTrigger.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (otherTrigger.isPushableInsideTrigger || isPushableInsideTrigger)
            return;

        if (collision.gameObject.tag == "Pushable")
        {
            collision.transform.position = new Vector3(otherTrigger.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            otherTrigger.isPushableInsideTrigger = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            isPushableInsideTrigger = false;
        }
    }
}
