using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour
{
    static bool isInsideTrigger = false;

    public Transform otherTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isInsideTrigger)
        {
            other.transform.position = new Vector2(otherTrigger.position.x, other.gameObject.transform.position.y);
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isInsideTrigger)
        {
            if (!other.GetComponent<Character>().ismoving)
                return;
            isInsideTrigger = false;
        }
    }
}
