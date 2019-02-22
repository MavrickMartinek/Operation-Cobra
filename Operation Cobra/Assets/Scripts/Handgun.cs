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
            this.transform.localRotation *= Quaternion.Euler(90, -90, -90); //Offsets gun rotation on hand.
            this.transform.localPosition = this.transform.parent.localPosition; //Matches gun postition to hand.
            this.transform.localPosition += new Vector3(0.03f, 0.01f, 0.06f); //Offsets gun location on hand.
            this.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f); //Corects gun scale.
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

        //Checks if trigger is pressed.
        if (!_Shot & inHand & OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown("mouse 0"))
        {
            if (shotCounter <= 0)
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
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, range))
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
