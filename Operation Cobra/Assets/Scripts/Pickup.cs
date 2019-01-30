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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            collision.transform.parent = transform;
            collision.transform.localScale = transform.localScale;
            collision.transform.localPosition = transform.localPosition;
           // collision.rigidbody.detectCollisions = false;
            collision.gameObject.GetComponent<RayCastShooting>().inHand = true;
        }
    }
}
