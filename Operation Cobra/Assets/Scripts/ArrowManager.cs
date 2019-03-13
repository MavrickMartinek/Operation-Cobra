using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {
    
    public float damage;

    private float score;

    public static ArrowManager instance;

    private GameObject currentArrow;

    public GameObject arrowPrefab;

    public GameObject rightController;
    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;

    private bool isAttached = false;

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
        AttachArrow();
        pullString();
	}

    private void pullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - rightController.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);

            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        float dist = (stringStartPoint.transform.position - rightController.transform.position).magnitude;

        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().hasBeenFired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = -currentArrow.transform.forward * 20f * dist;
        r.useGravity = true;

        //currentArrow.GetComponent<Collider>().isTrigger = false;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }

    public void AttachArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = rightController.transform;
            //arrowPrefab.transform.localScale = new Vector3(0.01f, 0.01f, 0.26f);
            currentArrow.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote);
            //currentArrow.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
            currentArrow.transform.localPosition = new Vector3(0f, 0f, 0.342f);
            currentArrow.transform.localRotation = Quaternion.Euler(new Vector3(currentArrow.transform.eulerAngles.x, currentArrow.transform.eulerAngles.y + 180, currentArrow.transform.eulerAngles.z));
            //currentArrow.transform.localRotation = Quaternion.identity;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
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
    }*/

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        //currentArrow.transform.localRotation = Quaternion.Euler(new Vector3(currentArrow.transform.eulerAngles.x, currentArrow.transform.eulerAngles.y + 180, currentArrow.transform.eulerAngles.z));
        currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == currentArrow)
        {
            score += 1;
            //Health target = col.gameObject.GetComponent<Health>();
            //f (target != null)
            //{
                //target.TakeDamage(damage);
            //}
        }
    }
}
