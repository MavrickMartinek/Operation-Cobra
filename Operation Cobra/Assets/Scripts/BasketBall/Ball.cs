using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Collider materialProps;
    private Vector3 thrownLocation;
    private bool ranOnce;
    public bool _Thrown;
    private bool inHand;
    private float timerPos = 0.5f;
    private float timer = 0.5f;
    // Use this for initialization
    void Start () {
        materialProps = this.GetComponent<Collider>();
        ranOnce = false;
        _Thrown = false;
 
	}
	
	// Update is called once per frame
	void Update () {
        if (!_Thrown & this.transform.parent != null)
        {
            this.transform.position = new Vector3(this.transform.parent.position.x, this.transform.position.y, this.transform.parent.position.z);
            if (ranOnce)
            {
                ToggleBounciness(1f);
            }
            ranOnce = true;
            if (!_Thrown & this.transform.position.y >= (this.transform.parent.position.y - 0.05f))
            {
                this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z);
            }

            if (!_Thrown & (this.transform.parent.parent.parent.localRotation.eulerAngles.x < 180f & this.transform.parent.parent.parent.localRotation.eulerAngles.x > 150f))
            {
                ReadyToThrow();
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                ReadyToThrow();
            }

            else if (inHand & OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
            {
                inHand = false;
                ToggleBounciness(0.5f);
                _Thrown = true;
                this.transform.parent = null;
                Rigidbody rigidbody = this.GetComponent<Rigidbody>();
                rigidbody.isKinematic = false;
                Vector3 throwVector = this.transform.position - thrownLocation;
                //rigidbody.angularVelocity 
                rigidbody.AddForce(throwVector * 200, ForceMode.Force);

               /* this.transform.LookAt(this.transform.position + this.transform.GetComponent<Rigidbody>().velocity);
                this.transform.forward = this.transform.parent.GetComponent<Rigidbody>().velocity;
                
                GetComponent<Rigidbody>().AddForce(GameObject.Find("CenterEyeAnchor").transform.forward * 1f);
                //this.transform.forward *= 2;*/
            }
        }

       if (this.transform.parent == null)
        {
            if (_Thrown)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    _Thrown = false;
                    ToggleBounciness(0.95f);
                    timer = 0.5f;
                }
                Debug.Log(timer);
            }
        }
        else
        {
            if (ranOnce)
            {
                ranOnce = false;
                ToggleBounciness(0.95f);
            }
        }

        if (this.transform.position.y <= -1)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.125f, this.transform.position.z);
        }
	}
    private void FixedUpdate()
    {
        timerPos -= Time.deltaTime;
        if (timerPos <= 0f)
        {
            thrownLocation = this.transform.position;
            timerPos = 0.5f;
        }
    }
    void ToggleBounciness(float b)
    {
            materialProps.material.bounciness = b;      
    }

   void ReadyToThrow()
    {
        Debug.Log("Ready to throw");
        ToggleBounciness(0f);
        inHand = true;
        this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z);
    }
}
