using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    void Start()
    {
        Destroy(this.gameObject, 3);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            Destroy(collision.gameObject, 1);
            Destroy(gameObject);
        }
    }
}
