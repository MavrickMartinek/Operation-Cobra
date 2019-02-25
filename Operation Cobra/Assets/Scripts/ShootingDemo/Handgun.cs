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

        if (inHand)
        {
            if (!ranOnce)
            {
                checkInput();
                ranOnce = true;
            }
            this.transform.localRotation = (this.transform.parent.localRotation * rotationCorrection); //Matches gun rotation to hand + offets it.
            this.transform.localPosition = (this.transform.parent.localPosition + positionCorrection); //Matches gun postition to hand + offsets it.
            this.transform.localScale = scaleCorrection;
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
        }

        //Checks if gun is in hand and trigger is pressed.
        if (this.inHand & ((!this.automaticFire & OVRInput.GetDown(this.inputHand) & !_Shot) | (this.automaticFire & OVRInput.Get(this.inputHand))))
        {   
            if (shotCounter <= 0 & currentAmmo > 0)
            {
                this.shotCounter = shotDelay;
                this.Shoot();
            }
        }

        if(this.currentAmmo <= 0)
        {
            this._Anim.SetBool("IsEmpty", true);
        }
        else
        {
            this._Anim.SetBool("IsEmpty", false);
        }

        if (this.inHand & OVRInput.GetDown(OVRInput.Button.One) & this.currentAmmo == 0)
        {
            this.currentAmmo = this.maxAmmo;
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
                target.TakeDamage(damage);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Create hit particle effect. 
            
            _Anim.SetBool("HasFired", true);
            this.currentAmmo -= 1;
        }
    }
    //Checks which hand is used
    void checkInput()
    {
        if (this.transform.parent.name == "HandRight")
        {
            this.inputHand = OVRInput.Button.SecondaryIndexTrigger;
            this.positionCorrection = positionCorrectionR;
        }
        else if (this.transform.parent.name == "HandLeft")
        {
            this.inputHand = OVRInput.Button.PrimaryIndexTrigger;
            this.positionCorrection = positionCorrectionL;
        }
    }
}
