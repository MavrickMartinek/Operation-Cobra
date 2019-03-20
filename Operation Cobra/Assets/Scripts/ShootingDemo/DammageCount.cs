using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour {

    public GameObject associatedWeapon;
    public Color scoreColor;
    public Color outOfAmmoColor;
    private int scoreCount;
    // Use this for initialization
    void Start()
    {
       // Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount = GameLoop._Score1;
        GetComponent<TextMesh>().text = scoreCount.ToString();
        if (scoreCount <= 0)
        {
            GetComponent<TextMesh>().color = outOfAmmoColor;
        }
        else
        {
            GetComponent<TextMesh>().color = scoreColor;
        }
    }
}
