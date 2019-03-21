using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    //https://forum.unity.com/threads/simple-way-to-have-smooth-jumping-without-rigid-body-finally.249488/
    private bool rdyToJump;
    // Use this for initialization
    void Start () {
		
	}
    private void FixedUpdate()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
