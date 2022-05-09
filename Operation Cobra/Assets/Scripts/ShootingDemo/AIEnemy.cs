/*
 * Author: Mavrick Martinek
 * Purpose: Controls AI behavior.
 */

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
        if (angleToPlayer >= -70 && angleToPlayer <= 70 && distanceToPlayer < 1f) //Checks if player is in the AI's field of vision.
        {
         
            Debug.Log("Player Visible");
            Debug.Log("Distance: " + distanceToPlayer);
            currentLerpTime = 0f;
            alert = true; //AI is now alert.
            spotted = true; //AI spotted the player
   
        }
        else //If player no longer is the AI's field of vision.
        {
            spotted = false;//AI no longer sees player.
            this.GetComponentInChildren<Shoot>().enabled = false;//AI stops shooting.
            lerpTime = GetLerpTime();
        }

        if (spotted)//Checks if AI spotted player.
        {
            AlertMode();
        }
        else if (alert && !spotted)//Checks if AI can't see player but is on alert.
        {
            if (!isMoving)//Checks if the AI is not moving.
            {
                MoveToLastPosition();//AI will move to where it last saw the player.
            }
            else if(isMoving) //Checks if the AI is supposed to be moving.
            {
                Rotate180(); //AI will rotate itself.
            }
        }
        else if (!alert && !spotted)
        {
            if (this.GetComponent<Health>()._Health < this.GetComponent<Health>().maxHealth) //Checks if AI has less than max health.
            {
                alert = true;
                isMoving = true;
            }
        }

	}

    void AlertMode()//Function called when AI is alerted to player.
    {
        /* lookRotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);
         strength = Mathf.Min(strength * Time.deltaTime, 1);
         this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, strength);
         Debug.Log("Alert"); */
        
        this.GetComponentInChildren<Shoot>().enabled = true;//Shoot script is called; AI starts shooting.
        this.transform.LookAt(Player.transform);//AI looks at player.
        this.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y, 0f);
        lastKnownPosition = Player.transform.position;//AI keeps the player's position in mind.
        isMoving = false;

        if(this.GetComponentInChildren<Shoot>().targetType != "MainCamera" || this.GetComponentInChildren<Shoot>().targetType != "Player") //Checks if AI is hitting something other than player.
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, (this.transform.localPosition + new Vector3(2, 0, 0)), Perc);
        }

    }

    void MoveToLastPosition() //Function called to move AI to last know player position.
    {
        if (this.transform.position.x != lastKnownPosition.x && this.transform.position.z != lastKnownPosition.z)
        {
            if (this.currentLerpTime > this.lerpTime)
            {
                isMoving = true;
                currentLerpTime = 0f;
            }
            this.GetComponentInChildren<Shoot>().enabled = false; //Stops shooting
            this.transform.position = Vector3.Lerp(this.transform.position, lastKnownPosition, Perc);//Moves AI to last known player position.
            this.transform.position = new Vector3(this.transform.position.x, 1.5f, this.transform.position.z);//Keeps AI in correct height.
            this.currentLerpTime += Time.deltaTime;
            Debug.Log("Lerping from " + this.transform.position + " to: " + lastKnownPosition + " step: " + Perc);
            
        }
     
    }
    void Rotate180()//Function called to spin AI around.
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
