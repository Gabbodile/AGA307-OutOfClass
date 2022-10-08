using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 1000f;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectileInstance;
            projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        } 
    }
}
