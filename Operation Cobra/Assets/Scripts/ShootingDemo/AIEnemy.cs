using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour {
    public bool alert;
    public bool spotted;
    private bool inFOV;
    public Camera AIVision;
    public GameObject Player;
    private Vector3 screenPoint;
    private Quaternion lookRotation;
    private float strength;
    private Vector3 lastKnownPosition;
    private float lerpTime;
    private float currentLerpTime = 0;
    private float Perc;
    private bool isMoving;
    // Use this for initialization
    void Start () {
        alert = false;
        this.GetComponentInChildren<Shoot>().enabled = false;
        lastKnownPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Perc = this.currentLerpTime / this.lerpTime;
        screenPoint = Player.transform.position - this.transform.position;
        float angleToPlayer = (Vector3.Angle(screenPoint, this.transform.forward));
        float distanceToPlayer = Player.transform.position.magnitude - this.transform.position.magnitude;
        // bool inFov = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 5 && screenPoint.y > 0 && screenPoint.y < 5;
        if (angleToPlayer >= -70 && angleToPlayer <= 70 && distanceToPlayer < 1f)
        {
         
            Debug.Log("Player Visible");
            Debug.Log("Distance: " + distanceToPlayer);
            currentLerpTime = 0f;
            alert = true;
            spotted = true;
   
        }
        else
        {
            spotted = false;
            this.GetComponentInChildren<Shoot>().enabled = false;
            lerpTime = GetLerpTime();
        }

        if (spotted)
        {
            AlertMode();
        }
        else if (alert && !spotted)
        {
            if (!isMoving)
            {
                MoveToLastPosition();
            }
            else if(isMoving)
            {
                Rotate180();
            }
        }
        else if (!alert && !spotted)
        {
            if (this.GetComponent<Health>()._Health < this.GetComponent<Health>().maxHealth)
            {
                alert = true;
                isMoving = true;
            }
        }

	}

    void AlertMode()
    {
        /* lookRotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);
         strength = Mathf.Min(strength * Time.deltaTime, 1);
         this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, strength);
         Debug.Log("Alert"); */
        
        this.GetComponentInChildren<Shoot>().enabled = true;
        this.transform.LookAt(Player.transform);
        this.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y, 0f);
        lastKnownPosition = Player.transform.position;
        isMoving = false;

        if(this.GetComponentInChildren<Shoot>().targetType != "MainCamera" || this.GetComponentInChildren<Shoot>().targetType != "Player")
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, (this.transform.localPosition + new Vector3(2, 0, 0)), Perc);
        }

    }

    void MoveToLastPosition()
    {
        if (this.transform.position.x != lastKnownPosition.x && this.transform.position.z != lastKnownPosition.z)
        {
            if (this.currentLerpTime > this.lerpTime)
            {
                isMoving = true;
                currentLerpTime = 0f;
            }
            this.GetComponentInChildren<Shoot>().enabled = false;
            this.transform.position = Vector3.Lerp(this.transform.position, lastKnownPosition, Perc);
            this.transform.position = new Vector3(this.transform.position.x, 1.5f, this.transform.position.z);
            this.currentLerpTime += Time.deltaTime;
            Debug.Log("Lerping from " + this.transform.position + " to: " + lastKnownPosition + " step: " + Perc);
            
        }
     
    }
    void Rotate180()
    {
        lerpTime = 1f;
        if (this.currentLerpTime > this.lerpTime)
        {
            isMoving = false;
            alert = false;
            currentLerpTime = 0f;
        }
        //float step = 100f * Time.deltaTime;
        this.currentLerpTime += Time.deltaTime;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation *= Quaternion.Euler(0, 180, 0), Time.deltaTime * 1f);
        //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, );
        Debug.Log("Rotating from: " + this.transform.rotation.y + " Lerp: " + currentLerpTime + "/" + lerpTime);
    }

    float GetLerpTime()
    {
        return (lastKnownPosition.magnitude - this.transform.position.magnitude) * 100;
    }
}
