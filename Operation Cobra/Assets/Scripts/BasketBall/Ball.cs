using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Collider materialProps;
    private Vector3 thrownLocation;
    private bool ranOnce;
    public bool _Thrown;
    private bool inHand;
    private float timerPos = 0.25f;
    private float timer = 1f;
    private OVRInput.Button grabButton;
    public OVRInput.Controller controller;
    private Quaternion lastRotation, currentRotation;

    // Use this for initialization
    void Start () {
        materialProps = this.GetComponent<Collider>();
        ranOnce = false;
        _Thrown = false;

    }
	
	// Update is called once per frame
	void Update () {
        
        if (this.transform.parent == null)
        {

        }
        else if (this.transform.parent.name == "HandRight")
        {
            grabButton = OVRInput.Button.SecondaryHandTrigger;
            controller = OVRInput.Controller.RTouch;
        }
        else if (this.transform.parent.name == "HandLeft")
        {
            grabButton = OVRInput.Button.PrimaryHandTrigger;
            controller = OVRInput.Controller.LTouch;
        }

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

            if (OVRInput.Get(grabButton))
            {
                ReadyToThrow();
            }

            else if (inHand & OVRInput.GetUp(grabButton))
            {
                throwObject();
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
                    timer = 1f;
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
            lastRotation = currentRotation;
            currentRotation = this.transform.rotation;
        }

        if (this.transform.position.y <= -1)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.125f, this.transform.position.z);
        }

        timerPos -= Time.deltaTime;
        if (timerPos <= 0f)
        {
            thrownLocation = this.transform.position;
            timerPos = 0.25f;
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Scorer")
        {
            GameLoop._Score1 += other.GetComponent<Scorer>().score;
            Debug.Log(GameLoop._Score1);
            //other.enabled = false;
        }
        else if (other.gameObject.name == "Scorer2")
        {
            GameLoop._Score2 += other.GetComponent<Scorer>().score;
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
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z);
    }

    void throwObject()
    {
        inHand = false;
        ToggleBounciness(0.5f);
        _Thrown = true;
        this.transform.parent = null;
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        Vector3 throwVector = this.transform.position - (thrownLocation);
        rigidbody.angularVelocity = GetAngularVelocity();
        Debug.Log("ThrownLocation: " + thrownLocation);
        Debug.Log("This.transform position: " + this.transform.position);
        Debug.Log("Throw Vector: " + throwVector);
        Transform trackingSpace = GameObject.Find("TrackingSpace").transform;
        rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(controller);
        //rigidbody.velocity = GameObject.Find("HandRight").GetComponent<Rigidbody>().velocity;
        rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
        rigidbody.AddForce((throwVector) * 10, ForceMode.Force);

        /* this.transform.LookAt(this.transform.position + this.transform.GetComponent<Rigidbody>().velocity);
         this.transform.forward = this.transform.parent.GetComponent<Rigidbody>().velocity;

         GetComponent<Rigidbody>().AddForce(GameObject.Find("CenterEyeAnchor").transform.forward * 1f);
         //this.transform.forward *= 2;*/
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
