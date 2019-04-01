using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float damage;
    public float range;
    public string targetType;
    public GameObject bulletExit;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;

    private float shotCounter;
    private float shotDelay = 0.5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
        else 
        {
            Fire();
            shotCounter = shotDelay;
        }   
	}

    void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletExit.transform.position, bulletExit.transform.forward, out hit, range))
        {
           // Debug.Log(hit.transform.name);
            Health target = hit.transform.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage);
                targetType = hit.transform.tag;
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Create hit particle effect. 
        }
    }
}
