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
    }

    private void dropObject()
    {
        _Weapon.transform.parent = null;
        _Weapon.gameObject.GetComponent<Weapon>().inHand = false;
        inHand = false;
        _Weapon.transform.position += new Vector3(0.05f, 0.05f, 0.05f);
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

