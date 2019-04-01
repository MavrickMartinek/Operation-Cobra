using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public Vector3 endPosition;
    public Vector3 startPostition;
    private bool loop = false;
    private float lerpTime = 2f;
    private float currentLerpTime = 0;
    // Use this for initialization
    void Start () {
        this.startPostition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float Perc = this.currentLerpTime / this.lerpTime;
        if (!loop)
        {

            //t += Time.deltaTime / timeToReach;
            //this.transform.position = Vector3.Lerp(startPostition, jumpHeight, 2 * Time.deltaTime);

            if (this.currentLerpTime >= this.lerpTime)
            {
                this.currentLerpTime = 0;
                this.loop = true;

            }
            this.currentLerpTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.startPostition, this.endPosition, Perc);

        }
        else if (this.loop)
        {
            if (this.currentLerpTime >= this.lerpTime)
            {
                this.currentLerpTime = 0;
                this.loop = false;

            }
            this.currentLerpTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.endPosition, this.startPostition, Perc);
        }
    }
}
