/*
 * Author: Mavrick Martinek
 * Purpose: Controls gun behaviour. 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage;
    public float range;

    public float shotDelay;
    private float shotCounter;
    private bool _Shot = false;

    public bool inHand = false;
    public bool automaticFire;
    public byte maxAmmo;
    public byte currentAmmo;

    public Vector3 positionCorrectionR;
    public Vector3 positionCorrectionL;
    public Quaternion rotationCorrection;
    public Vector3 scaleCorrection;

    private Vector3 positionCorrection;

    public GameObject bulletExit;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
    private Animator _Anim;
    private OVRInput.Button inputHand;
    private bool ranOnce = false;
    private Vector3 properRotation;
    private bool inRightHand;
	// Use this for initialization
	void Start () {
        _Anim = this.GetComponent<Animator>();  
	}
	
	// Update is called once per frame
	void Update () {
        //Checks if the gun is in the players' hand.
        if (inHand)
        {   
            if (!ranOnce)
            {
                checkInput(); //Checks in which hand the weapon is being held. 
                ranOnce = true;
            }
            this.transform.localRotation = (this.transform.parent.localRotation * rotationCorrection); //Matches gun rotation to hand + offets it.
            this.transform.localPosition = (this.transform.parent.localPosition + positionCorrection); //Matches gun postition to hand + offsets it.
            this.transform.localScale = scaleCorrection; //Corrects the weapon scale.
        }
        else if (!inHand)
        {
            this.ranOnce = false;
        }

        if (shotCounter > 0)
        {
            this._Shot = true;
            this.shotCounter -= Time.deltaTime;
        }
        else if (shotCounter < shotDelay)
        {
            this._Shot = false;
            this._Anim.SetBool("HasFired", false);
            //OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }

        //Checks if gun is in hand and trigger is pressed.
        if (this.inHand & ((!this.automaticFire & OVRInput.GetDown(this.inputHand) & !_Shot) | (this.automaticFire & OVRInput.Get(this.inputHand))))
        {
            Debug.Log("Shoot");
            if (shotCounter <= 0 & currentAmmo > 0)
            {
                this.shotCounter = shotDelay;
                this.Shoot();
            }
        }
        //Sets the animation of the weapon based on if it's empty.
        if(this.currentAmmo <= 0)
        {
            this._Anim.SetBool("IsEmpty", true);
        }
        else
        {
            this._Anim.SetBool("IsEmpty", false);
        }
        //Reload
        if (this.inHand & ((inRightHand & OVRInput.GetDown(OVRInput.Button.One) | (!inRightHand & OVRInput.GetDown(OVRInput.Button.Three)))))
        {
            this.currentAmmo = this.maxAmmo;
        }

        if (this.transform.position.y >= 2.25)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.down, ForceMode.Force);
        }

        if (this.transform.position.y <= 0.3)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
        }

        if (this.transform.position.z <= -4.6)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
        }
    }
    //Handles shooting.
    void Shoot()
    {
        RaycastHit hit; 
        if (Physics.Raycast(bulletExit.transform.position, bulletExit.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Health target = hit.transform.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage); //Deals damage to target.
                //OVRInput.SetControllerVibration(0.25f, 1f, OVRInput.Controller.RTouch);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Create hit particle effect. 
            
            _Anim.SetBool("HasFired", true); //Plays fire animation for gun.
            this.currentAmmo -= 1; //Drains ammunition.
        }
    }
    //Checks which hand is used
    void checkInput()
    {
        //Checks if weapon is in right or left hand; sets the inputs accordingly. 
        if (this.transform.parent.name == "HandRight")
        {
            this.inputHand = OVRInput.Button.SecondaryIndexTrigger;
            this.positionCorrection = positionCorrectionR;
            inRightHand = true;
        }
        else if (this.transform.parent.name == "HandLeft")
        {
            this.inputHand = OVRInput.Button.PrimaryIndexTrigger;
            this.positionCorrection = positionCorrectionL;
            inRightHand = false;
        }
    }
}
