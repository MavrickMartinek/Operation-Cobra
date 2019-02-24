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
        _Health = associatedPlayer.GetComponent<PlayerStat>().playerHealth;
        GetComponent<TextMesh>().text = _Health.ToString();
        if (_Health <= 0)
        {
            GetComponent<TextMesh>().color = deadHealthColor;
        }
        else
        {
            GetComponent<TextMesh>().color = normalHealthColor;
        }
    }
}
