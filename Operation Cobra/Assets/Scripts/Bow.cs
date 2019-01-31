using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public bool inHand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (inHand == true && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            this.transform.localRotation = GameObject.Find("Arrow").transform.localRotation;
            this.transform.localPosition = GameObject.Find("Arrow").transform.localPosition;
        }
	}
}
