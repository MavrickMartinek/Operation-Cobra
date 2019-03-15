using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    private float scoreOutput;

    public GameObject test;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Arrow testTwo = test.GetComponent<Arrow>();
        scoreOutput = testTwo.score;
	}
}
