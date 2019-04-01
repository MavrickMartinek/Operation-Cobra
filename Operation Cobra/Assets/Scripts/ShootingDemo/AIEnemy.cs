using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour {
    public bool alert;
    public Camera AIVision;
    public GameObject Player;
    private Vector3 screenPoint;
	// Use this for initialization
	void Start () {
        alert = false;
	}
	
	// Update is called once per frame
	void Update () {
        screenPoint = Player.transform.position;
        bool inFov = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (inFov)
        {
            Debug.Log("Player Visible");
            alert = true;
        }
        else
        {
            alert = false;
        }

	}
}
