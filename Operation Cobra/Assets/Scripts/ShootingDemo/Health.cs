/*
 * Author: Mavrick Martinek
 * Purpose: Attaching this script to an object will give it a "health" value, allowing it to be destroyed.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    //public Transform prefab;
    public float _Health; //Current health.
    public float maxHealth; //Maximum/Starting health.

    public void TakeDamage(float amount) //Function called when the object takes damage.
    {
        _Health -= amount;//Health is reduced per the amount passed in.
        //Instantiate(prefab, this.transform.position, this.transform.rotation);
        //Destroy(prefab.gameObject, 2);
        if (_Health <= 0f) //Checks if health is 0 or less.
        {
            if (this.gameObject.name != "CenterEyeAnchor")//Checks if the object is not the player.
            {
                Destroy(gameObject);//Object is destroyed.
                GameLoop._Score1 += 5; //Player score is increased.
                //prefab.GetComponent<TextMesh>().text = ammount.ToString();
               // Instantiate(prefab, (this.transform.position += new Vector3(10,5,-3)), this.transform.rotation);
                Debug.Log("Score: " + GameLoop._Score1);
            }
        }
    }
    // Use this for initialization
    void Start () {
        maxHealth = _Health; //The current health is set to the maximum when the object is initialized. 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
