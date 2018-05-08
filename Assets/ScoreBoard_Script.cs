using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ScoreBoard_Script : MonoBehaviour {

    public static bool checkingScore = false;
    public CanvasGroup scoreBoardUI;
    public TextMeshProUGUI p1Score;
    public TextMeshProUGUI p2Score;
    public TextMeshProUGUI p3Score;
    public TextMeshProUGUI p4Score;
    public ScoreManager sm;

    // Use this for initialization
    void Start () {
        scoreBoardUI = GameObject.Find("ScoreBoard").GetComponent<CanvasGroup>();
        scoreBoardUI.alpha = 0;
        p1Score = GameObject.Find("P1Score").GetComponent<TextMeshProUGUI>();
        p2Score = GameObject.Find("P2Score").GetComponent<TextMeshProUGUI>();
        p3Score = GameObject.Find("P3Score").GetComponent<TextMeshProUGUI>();
        p4Score = GameObject.Find("P4Score").GetComponent<TextMeshProUGUI>();
        sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("P1GameScoreButton") || Input.GetButtonDown("P2GameScoreButton") || Input.GetButtonDown("P3GameScoreButton") || Input.GetButtonDown("P4GameScoreButton"))
        {
            if (!checkingScore)
            {
                checkingScore = true;
                CheckScore();

            }
            else if (checkingScore)
            {
                checkingScore = false;
                StopCheckingScore();
            }
        }
    }

    public void CheckScore()
    {
        scoreBoardUI.GetComponent<CanvasGroup>().alpha = 1;

        if (GameObject.Find("Player 1"))
        {
            p1Score.text = "Player 1: " + sm.player1Score; 
        }
        
        if (GameObject.Find("Player 2"))
        {
            p2Score.text = "Player 2: " + sm.player2Score;
        }

        if (GameObject.Find("Player 3"))
        {
            p3Score.text = "Player 3: " + sm.player3Score;
        }

        if (GameObject.Find("Player 4"))
        {
            p4Score.text = "Player 4: " + sm.player4Score;
        }
    }

    public void StopCheckingScore()
    {
        scoreBoardUI.GetComponent<CanvasGroup>().alpha = 0;

    }
}