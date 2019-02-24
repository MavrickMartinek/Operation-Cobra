using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private bool inHand;
    private Collider _Weapon;
    private OVRInput.Button inputHand;
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
    }

    // Update is called once per frame
    void Update()
    {
        //Drop weapon
        if (OVRInput.GetUp(inputHand) & !OVRInput.GetDown(inputHand) & inHand)
        {

            _Weapon.transform.parent = null;
            _Weapon.gameObject.GetComponent<Handgun>().inHand = false;
            inHand = false;

            //   _Weapon.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f); //Corrects gun scale as it leaves hand.
            _Weapon.transform.position += new Vector3(0.1f, 0.1f, 0.1f);
            //Invoke("ToggleInHand", 1f);
            Debug.Log("Let go");
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            _Weapon.transform.position = new Vector3(1f,1.1f,2f);
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
            _Weapon = collision;
            //inHand = collision.gameObject.GetComponent<Handgun>().inHand;
            _Weapon.transform.parent = transform; //Transfers weapon to hand.
           //_Weapon.transform.localScale = transform.localScale;
           // _Weapon.transform.localPosition = transform.localPosition;
            // collision.rigidbody.detectCollisions = false;
            inHand = true;
            _Weapon.gameObject.GetComponent<Handgun>().inHand = inHand;
        }
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

