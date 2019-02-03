using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour {

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
            this.transform.localRotation = GameObject.Find("Right Hand").transform.localRotation; //Matches gun rotation to hand.
            this.transform.localRotation *= Quaternion.Euler(-90, 180, 0); //Offsets gun rotation on hand.
            this.transform.localPosition = GameObject.Find("Right Hand").transform.localPosition; //Matches gun postition to hand.
            this.transform.localPosition += new Vector3(0f, 0.25f, 0.75f); //Offsets gun location on hand.
            this.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f); //Corects gun scale.
        }
        
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }

        //Checks if trigger is pressed.
        if (!_Shot & inHand & OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown("mouse 0"))
        {
            if (shotCounter <= 0)
            {
                shotCounter = shotDelay;
                Shoot();
            }
        }
        //Checks if side button is pressed.
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) & inHand)
        {
            inHand = false; 
            this.transform.parent = null; //Removes gun from parent/hand. 
            this.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f); //Corrects gun scale as it leaves hand.
        }
    }
    //Handles shooting.
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
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Create hit particle effect. 

        }
    }
}
