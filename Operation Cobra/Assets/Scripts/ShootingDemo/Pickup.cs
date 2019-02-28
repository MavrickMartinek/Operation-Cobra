using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private bool inHand;
    private Collider _Weapon;
    private OVRInput.Button inputHand;
    private OVRTrackedRemote trackedObj;
    private bool _Throwing;
    private Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        if (this.name == "HandRight")
        {
            inputHand = OVRInput.Button.SecondaryHandTrigger;
        }
        else if (this.name == "HandLeft")
        {
            inputHand = OVRInput.Button.PrimaryHandTrigger;
        }
        inHand = _Weapon.gameObject.GetComponent<Weapon>().inHand;
    }

    // Update is called once per frame
    void Update()
    {
        //Drop weapon
        if ((OVRInput.GetUp(inputHand) & !OVRInput.GetDown(inputHand) & inHand) | (!GameLoop.gameRunning & !GameLoop.gameWon & !GameLoop.practiceMode))
        {
            dropObject();
        }
    }

    private void FixedUpdate()
    {
        if (_Throwing)
        {
            rigidbody.velocity = this.GetComponent<Rigidbody>().velocity;
            rigidbody.angularVelocity = (this.GetComponent<Rigidbody>().angularVelocity * 0.25f);
            Debug.Log("Thrown");
            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;

            _Throwing = false;
        }
    }
    //Detects if hand is in proximity of weapon. 
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("Collision");
        }
        if (collision.gameObject.tag == "Weapon" & OVRInput.GetDown(inputHand) & !inHand) //Checks if object touched is weapon.
        {
            _Weapon = collision; //Sets the collision as the weapon.
            _Weapon.transform.parent = transform; //Transfers weapon to hand.
            inHand = true;
            _Weapon.gameObject.GetComponent<Weapon>().inHand = inHand; //Tells the weapon script that it's in-hand.
        }

        if(collision.gameObject.tag == "Pickup")
        {
            if (!collision.GetComponent<Ball>()._Thrown)
            {
                collision.transform.parent = this.gameObject.transform;
                rigidbody = null;
            }
        }
    }

    private void dropObject()
    {
        rigidbody = _Weapon.GetComponent<Rigidbody>();
        _Weapon.transform.parent = null;
        _Throwing = true;
        _Weapon.gameObject.GetComponent<Weapon>().inHand = false;
        inHand = false;
        _Weapon.transform.position += new Vector3(0.05f, 0.05f, 0.05f);
        _Weapon = null;
        Debug.Log("Weapon dropped");
        
    }

    private void ToggleInHand()
    {
        if (inHand)
        {
            inHand = false;
        }
        else if (!inHand)
        {
            inHand = true;
        }
    }

}

