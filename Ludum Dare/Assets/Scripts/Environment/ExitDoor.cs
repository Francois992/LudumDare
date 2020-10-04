using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject shader;

    public bool isInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        shader.SetActive(false);
        isInteractable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shader.SetActive(true);
            isInteractable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shader.SetActive(false);
            isInteractable = false;
        }
    }
}
