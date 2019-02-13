using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * this script will handle the timer, scores, HUD, and game restarting
 */
public class GameplayManager : MonoBehaviour
{
    //Variables
    private int[] scores;
    public GameObject[] players;
    private GameObject blueText;
	private GameObject redText;
	private GameObject timeText;
    public GameObject gameOver;
    private float totalTime;
    private float currentTime;
    private float scoreTime;

    // Use this for initialization
    void Start()
    {
        scores = new int[2];
        totalTime = 100.0f;
		timeText = GameObject.Find ("Timer");
		redText = GameObject.Find ("Player2_Score");
		blueText = GameObject.Find ("Player1_Score");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        scoreTime = totalTime - currentTime;

        //Get all scores
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = players[i].GetComponent<Player>().GetScore();
        }

        //Update GUI text
        timeText.GetComponent<Text>().text = "Time\n" + (Mathf.Round(scoreTime));
		blueText.GetComponent<Text>().text = (scores[0]).ToString();
		redText.GetComponent<Text>().text = (scores[1]).ToString();


        //Detect if it was a tie or who won
        if (scoreTime < 0 && gameOver.activeSelf == false)
        {
            gameOver.SetActive(true);
            if (scores[0] < scores[1])
                gameOver.transform.GetChild(1).GetComponent<Text>().text = "Player 2 Wins! ";
            else if (scores[0] > scores[1])
                gameOver.transform.GetChild(1).GetComponent<Text>().text = "Player 1 Wins! ";
            else if (scores[0] == scores[1])
                gameOver.transform.GetChild(1).GetComponent<Text>().text = "Wait you guys tied!?! How is that even possible??? ";

            gameOver.transform.GetChild(3).GetComponent<Text>().text = "Player 1: " + scores[0];
            gameOver.transform.GetChild(4).GetComponent<Text>().text = "Player 2: " + scores[1];
        }

        if (gameOver.activeSelf)
        {
            if (Input.GetAxis("P1_Throw_L") > 0.0f || Input.GetAxis("P1_Throw_R") > 0.0f || Input.GetAxis("P2_Throw_L") > 0.0f || Input.GetAxis("P2_Throw_R") > 0.0f)
                LoadMenu();
        }


    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}