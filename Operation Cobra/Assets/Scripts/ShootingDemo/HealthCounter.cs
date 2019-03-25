using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter : MonoBehaviour {

    public GameObject associatedPlayer;
    private float _Health;
    public Color deadHealthColor;
    public Color normalHealthColor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _Health = associatedPlayer.GetComponentInChildren<Health>()._Health;
        
        if (_Health <= 0)
        {
            GetComponent<TextMesh>().color = deadHealthColor;
            GetComponent<TextMesh>().text = "Down";
        }
        else
        {
            GetComponent<TextMesh>().color = normalHealthColor;
            GetComponent<TextMesh>().text = _Health.ToString();
        }
    }
}
