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
        Destroy(prefab.gameObject, 2);
        if (_Health <= 0f)
        {
            if (this.gameObject.name != "CenterEyeAnchor")
            {
                Destroy(gameObject);
                GameLoop._Score += 5;
                prefab.GetComponent<TextMesh>().text = ammount.ToString();
                Instantiate(prefab, (this.transform.position += new Vector3(10,5,-3)), this.transform.rotation);
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
