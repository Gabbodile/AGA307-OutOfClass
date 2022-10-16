using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.CompareTag("Player"))
        {
            sphere.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying");
        if (other.CompareTag("Player"))
        {
            sphere.transform.localScale += Vector3.one * 0.01f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (other.CompareTag("Player"))
        {
            sphere.transform.localScale = Vector3.one;
            sphere.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
