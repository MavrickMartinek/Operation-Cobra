using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooting : MonoBehaviour {

    public float damage;
    public float range;

    public float shotDelay;
    private float shotCounter;
    private bool _Shot = false;

    public bool inHand = false;

    public GameObject gun;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;

    private Vector3 properRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (inHand)
        {
            this.transform.localRotation = GameObject.Find("Right Hand").transform.localRotation;
            this.transform.localRotation *= Quaternion.Euler(-90, 180, 0);
            this.transform.localPosition = GameObject.Find("Right Hand").transform.localPosition;
            this.transform.localPosition += new Vector3(0f, 0.25f, 0.75f);
            this.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        }
        
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }

        if (!_Shot & inHand & OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown("mouse 0"))
        {
            if (shotCounter <= 0)
            {
                shotCounter = shotDelay;
                Shoot();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) & inHand)
        {
            inHand = false;
            this.transform.parent = null;
            this.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f);
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
}
