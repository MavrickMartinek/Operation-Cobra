using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Collider materialProps;
    private bool ranOnce;
    public bool _Thrown;
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
            /*else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
            {
                ToggleBounciness(0.2f);
                _Thrown = true;
                //this.transform.forward = this.transform.parent.GetComponent<Rigidbody>().velocity;
                this.transform.parent = null;
                GetComponent<Rigidbody>().AddForce(GameObject.Find("CenterEyeAnchor").transform.forward * 1f);
                //this.transform.forward *= 2;
                WaitUntilPickup();
            }*/
        }

        if (this.transform.parent == null)
        {
            if (_Thrown)
            {
                
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

    void ToggleBounciness(float b)
    {
            materialProps.material.bounciness = b;      
    }

   void ReadyToThrow()
    {
        Debug.Log("Ready to throw");
        ToggleBounciness(0f);
        this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z);
    }

    IEnumerator WaitUntilPickup()
    {
        yield return new WaitForSeconds(1.5f);
        _Thrown = false;
    }
}
