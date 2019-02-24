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
    public bool automaticFire;
    public byte maxAmmo;
    public byte currentAmmo;

    public Vector3 postionCorrection;
    public Quaternion rotationCorrection;
    public Vector3 scaleCorrection;

    public GameObject bulletExit;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
    private Animator _Anim;

    private Vector3 properRotation;
	// Use this for initialization
	void Start () {
        _Anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (inHand)
        {
            this.transform.localRotation = this.transform.parent.localRotation; //Matches gun rotation to hand.
            // this.transform.localRotation *= Quaternion.Euler(90, -90, -90); //Offsets gun rotation on hand.
            this.transform.localRotation *= rotationCorrection;
            this.transform.localPosition = this.transform.parent.localPosition; //Matches gun postition to hand.
            //  this.transform.localPosition += new Vector3(0.03f, 0.01f, 0.06f); //Offsets gun location on hand.
            this.transform.localPosition += postionCorrection;
            //  this.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f); //Corects gun scale.
            this.transform.localScale += scaleCorrection;
        }

        if (shotCounter > 0)
        {
            _Shot = true;
            shotCounter -= Time.deltaTime;
        }
        else if (shotCounter < shotDelay)
        {
            _Shot = false;
            _Anim.SetBool("HasFired", false);
        }

        //Checks if gun is in hand and trigger is pressed.
        if (inHand & OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown("mouse 0"))
        {   //Checks if gun should be fired according to fire rate and if its automatic or not
            if ((!automaticFire & !_Shot) | (automaticFire & shotCounter <= 0) & shotCounter <= 0 & currentAmmo > 0)
            {
                shotCounter = shotDelay;
                Shoot();
            }
            
        }
        else
        {
           // _Anim.SetBool("HasFired", false);
        }
       
    }
    //Handles shooting.
    void Shoot()
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
            
            _Anim.SetBool("HasFired", true);
        }
    }
}
