using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    //private OVRPlugin.p

    public static ArrowManager instance;

    private GameObject currentArrow;

    public GameObject arrowPrefab;

    public GameObject stringAttachPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public void AttachArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bow")
        {
            collision.transform.parent = transform;
            collision.transform.localScale = transform.localScale;
            collision.transform.localPosition = transform.localPosition;
            // collision.rigidbody.detectCollisions = false;
            //collision.gameObject.GetComponent<>().inHand = true;
            collision.gameObject.GetComponent<Bow>().inHand = true;
        }
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.transform.parent = stringAttachPoint.transform;
    }
}
