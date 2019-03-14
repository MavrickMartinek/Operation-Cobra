using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public Transform prefab;
    public float _Health;

    public void TakeDamage(float ammount)
    {
        _Health -= ammount;
        Instantiate(prefab, this.transform.position, this.transform.rotation);
        Destroy(prefab, 2);
        if (_Health <= 0f)
        {
            if (this.gameObject.name != "CenterEyeAnchor")
            {
                Destroy(gameObject);
                GameLoop._Score += 5;
                Instantiate(prefab, this.transform.position, this.transform.rotation);
                Destroy(prefab, 2);
                Debug.Log("Score: " + GameLoop._Score);
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
