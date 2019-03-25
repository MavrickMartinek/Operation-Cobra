using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public static int scoreOutput;

    public GameObject test;
    public GameObject victory;

	// Use this for initialization
	void Start () {
        victory.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //Arrow testTwo = test.GetComponent<Arrow>();
        //scoreOutput = testTwo.score;
        GetComponent<TextMesh>().text = scoreOutput.ToString();
        if(scoreOutput >= 10)
        {
            victory.SetActive(true);
        }
	}
}
