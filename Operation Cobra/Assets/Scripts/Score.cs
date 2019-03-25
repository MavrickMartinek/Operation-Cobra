using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public static int scoreOutput;

    public GameObject test;
    public GameObject victory;
	
	void Update () {

        GetComponent<TextMesh>().text = scoreOutput.ToString();
        if(scoreOutput >= 8)
        {
            GetComponent<TextMesh>().text = "Victory";
        }
	}
}
