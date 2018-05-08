﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{

    public int objectHealth;
    public int objectDmg;
    public int playerNumThrown;
    public int totalDmg;
    public Rigidbody rb;

    public ScoreManager playerScore;
    public GameManager gm;
    

    //needed to stop multiple collisions
    private bool hasHit;
    static float hitDelay = 0.4f;

    void Start()
    {
        playerScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (playerNumThrown)
            {
                case 1:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                hasHit = true;
                                StartCoroutine("resetHit");
                                Debug.Log(collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth);
                                if (collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth <= 0)
                                {
                                    playerScore.player1Score += 1;
                                    Debug.Log(playerScore.player1Score);
                                    hasHit = true;
                                    StartCoroutine("resetHit");
                                    gm.PlayerDeath(GameObject.Find("Player 2"));
                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth == 0)
                                {
                                    playerScore.player1Score += 1;
                                    Debug.Log(playerScore.player1Score);
                                    gm.PlayerDeath(GameObject.Find("Player 3"));

                                }

                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                               
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth == 0)
                                {
                                    playerScore.player1Score += 1;
                                    Debug.Log(playerScore.player1Score);
                                    gm.PlayerDeath(GameObject.Find("Player 4"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 2:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 1":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 1");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth == 0)
                                {
                                    playerScore.player2Score += 1;
                                    Debug.Log(playerScore.player2Score);
                                    gm.PlayerDeath(GameObject.Find("Player 1"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth == 0)
                                {
                                    playerScore.player2Score += 1;
                                    Debug.Log(playerScore.player2Score);
                                    gm.PlayerDeath(GameObject.Find("Player 3"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                                                                                            
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth == 0)
                                {
                                    playerScore.player2Score += 1;
                                    Debug.Log(playerScore.player2Score);
                                    gm.PlayerDeath(GameObject.Find("Player 4"));

                                }

                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 3:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 1":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 1");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth == 0)
                                {
                                    playerScore.player3Score += 1;
                                    Debug.Log(playerScore.player3Score);
                                    gm.PlayerDeath(GameObject.Find("Player 1"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth == 0)
                                {
                                    playerScore.player3Score += 1;
                                    Debug.Log(playerScore.player3Score);
                                    gm.PlayerDeath(GameObject.Find("Player 2"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 4":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 4");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP4_Script>().playerHealth == 0)
                                {
                                    playerScore.player3Score += 1;
                                    Debug.Log(playerScore.player3Score);
                                    gm.PlayerDeath(GameObject.Find("Player 4"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
                case 4:
                    Debug.Log("found stage one");
                    switch (collision.gameObject.name)
                    {
                        case "Player 1":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 1");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP1_Script>().playerHealth == 0)
                                {
                                    playerScore.player4Score += 1;
                                    Debug.Log(playerScore.player4Score);
                                    gm.PlayerDeath(GameObject.Find("Player 1"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 2":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 2");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP2_Script>().playerHealth == 0)
                                {
                                    playerScore.player4Score += 1;
                                    Debug.Log(playerScore.player4Score);
                                    gm.PlayerDeath(GameObject.Find("Player 2"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                        case "Player 3":
                            if (hasHit == false)
                            {
                                Debug.Log("Hit player 3");
                                totalDmg = objectDmg;
                                collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth -= totalDmg;
                                
                                hasHit = true;
                                StartCoroutine("resetHit");
                                if (collision.gameObject.GetComponent<FPSControllerP3_Script>().playerHealth == 0)
                                {
                                    playerScore.player4Score += 1;
                                    Debug.Log(playerScore.player4Score);
                                    gm.PlayerDeath(GameObject.Find("Player 3"));

                                }
                                playerNumThrown = 0;
                            }
                            break;
                    }
                    break;
            }

        }


    }

    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(hitDelay);
        hasHit = false;
    }



}

