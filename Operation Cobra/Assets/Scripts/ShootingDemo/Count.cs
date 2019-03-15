using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour {
    private float _Score1;
    private float _Score2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _Score1 = GameLoop._Score1;
        _Score2 = GameLoop._Score2;
        GetComponent<TextMesh>().text = _Score1.ToString() + ":" + _Score2.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
