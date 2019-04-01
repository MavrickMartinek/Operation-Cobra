using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    [SerializeField]
    Transform[] wayPoints;
    int currentWayPoint = 0;
    Rigidbody rigidB;
    [SerializeField]
    float moveSpeed = 5f;

	// Use this for initialization
	void Start () {
        rigidB = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(Vector3.Distance(transform.position, wayPoints[currentWayPoint].position) < .25f)
        {
            currentWayPoint += 1;
            currentWayPoint = currentWayPoint % wayPoints.Length;
        }
        Vector3 dir = (wayPoints[currentWayPoint].position - transform.position).normalized;
        rigidB.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
    }
}
