using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Detects collision.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon") //Checks if object touched is weapon.
        {
            collision.transform.parent = transform; //Transfers weapon to hand.
            collision.transform.localScale = transform.localScale;
            collision.transform.localPosition = transform.localPosition;
           // collision.rigidbody.detectCollisions = false;
            collision.gameObject.GetComponent<Handgun>().inHand = true;
        }
    }
}
