using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float score;

    private bool isAttached = false;

    private bool isFired = false;
	
	void Update () {
		if (isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
	}

    public void hasBeenFired()
    {
        isFired = true;
    }

    void OnTriggerStay()
    {
        AttachArrow();
    }

    void OnTriggerEntered()
    {
        AttachArrow();
    }

    private void AttachArrow()
    {
        if(!isAttached && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            ArrowManager.instance.AttachBowToArrow();
            isAttached = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Score.scoreOutput += 2;
            GameObject.Destroy(col.gameObject);
        }
    }
}
