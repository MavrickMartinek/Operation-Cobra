using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    //https://forum.unity.com/threads/simple-way-to-have-smooth-jumping-without-rigid-body-finally.249488/
    private bool _Jump = false;
    private bool _Fall = false;
    public Vector3 jumpHeight = new Vector3(0, 2.25f, 0);
    private Vector3 startPostition;
    private float distance = 5f;
    private float t = 0f;
    public float timeToReach = 20f;

    private float lerpTime = 1.25f;
    private float currentLerpTime = 0;

    // Use this for initialization
    void Start () {
        
	}
    private void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update () {

        float Perc = currentLerpTime / lerpTime;
        
        if (!_Jump & !_Fall & this.transform.position.y < 0.8)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                _Jump = true;
                //this.GetComponent<OVRPlayerController>().Jump();
                startPostition = this.transform.position;
                //jumpHeight = Mathf.Sqrt(2 * 2.25f * 1);
                Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                Transform trackingSpace = GameObject.Find("TrackingSpace").transform;
                //Debug.Log("Tracking X: " + trackingSpace.rotation.x + " Tracking Z: " + trackingSpace.rot.z);
                //jumpHeight.x = trackingSpace.position.x + primaryAxis.x;
                //jumpHeight.z = trackingSpace.position.z + primaryAxis.y;
                jumpHeight.x = this.startPostition.x;
                jumpHeight.z = this.startPostition.z;
                //jumpHeight = startPostition + Vector3.up * distance;
            }
        }
        else if (_Jump)
        {

            //t += Time.deltaTime / timeToReach;
            //this.transform.position = Vector3.Lerp(startPostition, jumpHeight, 2 * Time.deltaTime);
            
            if (currentLerpTime >= lerpTime)
            {
                //currentLerpTime = 0;
                _Jump = false;
                _Fall = true;
            }
            currentLerpTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPostition, jumpHeight, Perc);
            
            t = 0f;
            
        }
        else if (_Fall)
        {
            currentLerpTime -= Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPostition, jumpHeight, Perc);
            if (currentLerpTime <= 0f)
            {
                _Fall = false;
                currentLerpTime = 0f;
            }
        }
	}
}
