using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject hitSparks;
    public LineRenderer laser;
    public float projectileSpeed = 1000f;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireProjectile();
        }  
        
        if (Input.GetButtonDown("Fire2"))
        {
            fireRaycast();
        } 
    }

    void fireProjectile()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    void fireRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("We hit " + hit.collider.name + " at point " + hit.point + " which was " + hit.distance + " units away.");
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            GameObject party = Instantiate(hitSparks, hit.point, transform.rotation);
            Destroy(party, 2);

        }    
    }
}
