using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour {

    public Rigidbody Projectile;
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown("mouse 0"))
        {
            Rigidbody newProjectile = Instantiate(Projectile, transform.position, transform.rotation) as Rigidbody;

            newProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, Speed));
        }
    }
}
