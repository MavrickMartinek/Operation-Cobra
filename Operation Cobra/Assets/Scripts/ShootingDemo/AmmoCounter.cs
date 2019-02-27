using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour {

    public GameObject associatedWeapon;
    public Color ammoColor;
    public Color outOfAmmoColor;
    private byte ammoCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ammoCount = associatedWeapon.GetComponent<Weapon>().currentAmmo;
        GetComponent<TextMesh>().text = ammoCount.ToString();
        if (ammoCount <= 0)
        {
            GetComponent<TextMesh>().color = outOfAmmoColor;
        }
        else
        {
            GetComponent<TextMesh>().color = ammoColor;
        }
	}
}
