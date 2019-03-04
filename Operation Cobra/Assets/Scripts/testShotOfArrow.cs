using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShotOfArrow : MonoBehaviour {

    public GameObject arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        arrowSend();
	}

    private void arrowSend()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            arrow.transform.parent = null;
            Rigidbody r = arrow.GetComponent<Rigidbody>();
            r.velocity = arrow.transform.forward * 10f;
            r.useGravity = true;
        }
    }
}
