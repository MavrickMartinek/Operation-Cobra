/*
 * Purpose: Handles overall game loop; when to start/ end a match.
 * 
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {

    public float timeLimit;
    public int scoreGoal;
    private float _Timer;
    private GameObject playerObject;
    GameObject[] enemyObjects;
    private float playerHealth;
    public static int _Score1;
    public static int _Score2;
    public static bool gameRunning;
    public static bool practiceMode = true;
    public static bool gameWon;
	// Use this for initialization
	void Start () {
        playerObject = GameObject.Find("CentreEyeController");
        playerHealth = playerObject.GetComponentInChildren<Health>()._Health;
        gameRunning = false;
        practiceMode = true;

        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
  
        foreach (GameObject obj in enemyObjects)
        {
            Debug.Log("object");
            Debug.Log(obj.gameObject.name);
        }
        startGame();
    }
	
	// Update is called once per frame
	void Update () {
		
        /* if (_Timer == 0f | _Score >= scoreGoal | playerHealth <= 0 | enemyObjects.Length <= 0)
         {
             endGame();
         }*/

       /* for (int i = 0; i <= this.enemyObjects.Length; i++)
        {
            if (enemyObjects[i].gameObject.GetComponent<Health>()._Health <= 0)
            {
                gameWon = true;
                endGame();
            }
        }*/
	}

    private void FixedUpdate()
    {
        if (gameRunning)
        {
            _Timer -= Time.deltaTime;
        }
        //Debug.Log(_Timer);
        //Debug.Log(enemyObjects[1].name);
    }
    void startGame()
    {
        practiceMode = false;
        gameRunning = true;
        gameWon = false;
        playerHealth = 100;
        _Timer = timeLimit;
    }

    void endGame()
    {
        gameRunning = false;
        if (_Score1 >= scoreGoal)
        {
            gameWon = true;
            winGame();
        }
        else
        {
            gameWon = false;
            loseGame();
        }
    }

    void winGame()
    {

    }

    void loseGame()
    {
        Debug.Log("Lost the game");
    }
}
