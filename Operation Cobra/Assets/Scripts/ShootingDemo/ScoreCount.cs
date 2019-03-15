using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<TextMesh>().text = GameLoop._Score1.ToString(); ;
    }
}
