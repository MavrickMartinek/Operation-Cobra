using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float _Health;

    public void TakeDamage(float ammount)
    {
        _Health -= ammount;
        if (_Health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
