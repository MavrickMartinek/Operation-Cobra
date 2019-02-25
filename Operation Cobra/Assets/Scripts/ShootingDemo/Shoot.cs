using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float damage;
    public float range;

    public GameObject bulletExit;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;

    private float shotCounter;
    private float shotDelay = 1;
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
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Create hit particle effect. 
        }
    }
}
