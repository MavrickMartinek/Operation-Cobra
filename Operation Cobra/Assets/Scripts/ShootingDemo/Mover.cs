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
        startPostition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float Perc = currentLerpTime / lerpTime;
        if (!loop)
        {

            //t += Time.deltaTime / timeToReach;
            //this.transform.position = Vector3.Lerp(startPostition, jumpHeight, 2 * Time.deltaTime);

            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = 0;
                loop = true;

            }
            currentLerpTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPostition, endPosition, Perc);

        }
        else if (loop)
        {
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = 0;
                loop = false;

            }
            currentLerpTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(endPosition, startPostition, Perc);
        }
    }
}
