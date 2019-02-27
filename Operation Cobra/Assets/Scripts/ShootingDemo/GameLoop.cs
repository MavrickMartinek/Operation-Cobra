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
    private GameObject[] enemyObjects;
    private float playerHealth;
    public static int _Score;
    public static bool gameRunning;
    public static bool practiceMode = true;
    public static bool gameWon;
	// Use this for initialization
	void Start () {
        playerObject = GameObject.Find("OVRPlayerController");
        playerHealth = playerObject.GetComponentInChildren<Health>()._Health;
        gameRunning = false;
        practiceMode = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameRunning)
        {
            _Timer -= Time.deltaTime;
        }
        if (_Timer == 0f | _Score >= scoreGoal | playerHealth <= 0 | enemyObjects.Length <= 0)
        {
            endGame();
        }

        for (int i = 0; i <= enemyObjects.Length; i++)
        {
            if (enemyObjects[i].GetComponent<Health>()._Health <= 0)
            {
                gameWon = true;
                endGame();
            }
        }
	}
    void startGame()
    {
        practiceMode = false;
        gameRunning = true;
        gameWon = false;
        playerHealth = 100;
        _Timer = timeLimit;

       enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void endGame()
    {
        gameRunning = false;
        if (_Score >= scoreGoal)
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

    }
}
